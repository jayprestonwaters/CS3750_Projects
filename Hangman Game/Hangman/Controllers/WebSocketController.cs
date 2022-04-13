using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Hangman.Pages;
using Hangman.Models;
using Newtonsoft.Json;
using Hangman.Data;

namespace WebSocketsSample.Controllers
{
    #region snippet
    public class WebSocketController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public Dictionary<int, string> _words;
        public string currentWord;
        List<char> letters = new();
        int counter = 0;
        int loserCount = 0;
        int uId;

    public WebSocketController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/ws")]
        public async Task Get()
        {          
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                uId = (int)HttpContext.Session.GetInt32("userID");
                LoadJson();
                Random r = new Random();
                int rand = r.Next(1, 51);

                currentWord = _words.Where(w => w.Key == rand).First().Value;

                using WebSocket webSocket = await 
                                   HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(HttpContext, webSocket);

                
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }


        }
        #endregion

        private async Task Echo(HttpContext httpContext, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var answerBuff = new byte[1024 * 4];
            
           
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                answerBuff = System.Text.Encoding.ASCII.GetBytes(Guess(currentWord, System.Text.Encoding.ASCII.GetChars(buffer)));
                if (answerBuff.Equals("Win"))
                {                
                    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                }           
                await webSocket.SendAsync(new ArraySegment<byte>(answerBuff), WebSocketMessageType.Text, true, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        public string Guess(string currWord, char[] b)
        {
            char c = b.FirstOrDefault();                      
            string message = "";
            
            // for each letter in currentWord add to list letters
            // if current word contains result/guess send correct or wrong message
            for (int i = 0; i < 1; i++)
            {
                if(counter == currWord.Length-1)
                {
                    saveScoreAsync(uId, counter, currWord);
                    message = "You Win!!!";
                    break;
                }
                if(loserCount == 5)
                {
                    saveScoreAsync(uId, counter, currWord);
                    message = "You Lost!!!";
                    break;
                }
                if (currWord.Contains(c))
                {                    
                    if (letters.Contains(c))
                    {
                        loserCount++;
                        message = "Used letter";
                        break;
                    }
                    letters.Add(c);
                    var x = HttpContext.Session.GetString("corGuess");
                    HttpContext.Session.SetString("corGuess", x + c);
                    for (int j = 0; j < currWord.Length; j++)
                    {
                        if(char.ToLower(currWord[j]) == char.ToLower(c))
                        {
                            counter++;
                        }                        
                    }                    
                    message = "Correct";
                    break;
                }
                loserCount++;
                message = "Incorrect";
                
            }
            return message;
        }       
        public async Task saveScoreAsync(int id, int score, string currW)
        {
            var newScore = new Scores
            {
                UserId = id,
                Score = score,
                WordPlayed = currW,
            };

            _context.Scores.Add(newScore);
            await _context.SaveChangesAsync();
        }
        public void LoadJson()
        {
            using (StreamReader r = new("dictionary.json"))
            {
                string json = r.ReadToEnd();
                Dictionary<int, string> words = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);

                _words = words;
            }
        }
    }
}
