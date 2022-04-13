using System.Linq;
using System.Threading.Tasks;
using BlazorWebAssemblySignalRApp.Shared.Models.Speed;
using Microsoft.AspNetCore.SignalR;

namespace BlazorWebAssemblySignalRApp.Server.Hubs
{
    public static class UserManager
    {
        public static Dictionary<int, string> Connections = new();
        public static int MaxId = 0;
    }
    
    public class SpeedHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            if (UserManager.MaxId < 1 || !UserManager.Connections.ContainsKey(1))
            {
                UserManager.Connections.Add(1, Context.ConnectionId);
                UserManager.MaxId++;
            }
            else if (UserManager.MaxId < 2 || !UserManager.Connections.ContainsKey(2))
            {
                UserManager.Connections.Add(2, Context.ConnectionId);
                UserManager.MaxId++;
            }
            else
            {
                UserManager.Connections.Add(UserManager.MaxId, Context.ConnectionId);
                UserManager.MaxId++;
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var users = UserManager.Connections.Where(c => c.Value.Equals(Context.ConnectionId));

            foreach (var user in users)
            {
                UserManager.Connections.Remove(user.Key);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task BeginGame(Table table)
        {
            await SendTable(table);
        }

        public async Task DrawCard(Table table, string playerGuid)
        {
            int playerNum = GetPlayerNumber(playerGuid);

            if (playerNum == 1)
            {
                if (table.PlayerOneDraw.Count > 0 && table.PlayerOneHand.Count < 5) 
                {
                    Card newCard = table.PlayerOneDraw.Pop();
                    table.PlayerOneHand.Add(newCard);
                }
            }
            else if (playerNum == 2)
            {
                if (table.PlayerTwoDraw.Count > 0 && table.PlayerTwoHand.Count < 5)
                {
                    Card newCard = table.PlayerTwoDraw.Pop();
                    table.PlayerTwoHand.Add(newCard);
                }
            }

            await SendTable(table);
        }

        public async Task PlayCard(Table table, Card card, string playerGuid, bool pileOne) 
        {
            int playerNum = GetPlayerNumber(playerGuid);

            if (playerNum == 1 && pileOne)
            {
                var crd = table.PlayerOneHand.Where(c => c.Value == card.Value && c.Suit == card.Suit).FirstOrDefault();
                if (crd is not null)
                {
                    table.PlayerOneHand.Remove(crd);
                    table.PlayOne.Push(crd);
                }
                //    table.PlayerOneHand.Remove(card);
                //table.PlayOne.Push(card);
            }
            else if (playerNum == 1 && !pileOne)
            {
                var crd = table.PlayerOneHand.Where(c => c.Value == card.Value && c.Suit == card.Suit).FirstOrDefault();
                if (crd is not null)
                {
                    table.PlayerOneHand.Remove(crd);
                    table.PlayTwo.Push(crd);
                }

                //table.PlayerOneHand.Remove(card);
                //table.PlayTwo.Push(card);
            }
            else if (playerNum == 2 && pileOne)
            {
                var crd = table.PlayerTwoHand.Where(c => c.Value == card.Value && c.Suit == card.Suit).FirstOrDefault();
                if (crd is not null)
                {
                    table.PlayerTwoHand.Remove(crd);
                    table.PlayOne.Push(crd);
                }

                //table.PlayerTwoHand.Remove(card);
                //table.PlayOne.Push(card);
            }
            else if (playerNum == 2 && !pileOne)
            {
                var crd = table.PlayerTwoHand.Where(c => c.Value == card.Value && c.Suit == card.Suit).FirstOrDefault();
                if (crd is not null)
                {
                    table.PlayerTwoHand.Remove(crd);
                    table.PlayTwo.Push(crd);
                }

                //table.PlayerTwoHand.Remove(card);
                //table.PlayTwo.Push(card);
            }

            await SendTable(table);
        }

        public async Task PullCard(Table table)
        {
            if (table.PullOne.Count < 1 || table.PullTwo.Count < 1)
            {
                table = ShufflePlayAndPull(table);
            }
            else
            {
                table.PlayOne.Push(table.PullOne.Pop());
                table.PlayTwo.Push(table.PullTwo.Pop());
            }

            await SendTable(table);
        }

        public Table ShufflePlayAndPull(Table table)
        {
            while (table.PlayOne.Count > 0)
            {
                table.Discard.Add(table.PlayOne.Pop());
            }

            while (table.PlayTwo.Count > 0)
            {
                table.Discard.Add(table.PlayTwo.Pop());
            }

            while (table.PullOne.Count > 0)
            {
                table.Discard.Add(table.PullOne.Pop());
            }

            while (table.PullTwo.Count > 0)
            {
                table.Discard.Add(table.PullTwo.Pop());
            }

            table.Shuffle(table.Discard);

            table.PlayOne.Push(table.Discard[0]);
            table.PlayTwo.Push(table.Discard[1]);

            for (int i = 2; i < 8; i++)
            {
                table.PullOne.Push(table.Discard[i]);
            }
            for (int i = 8; i < 12; i++)
            {
                table.PullTwo.Push(table.Discard[i]);
            }

            table.Discard.Clear();

            return table;
        }

        public async Task SendTable(Table table)
        {
            Table newTable = AddPlayers(table);
            await Clients.All.SendAsync("ReceiveUpdatedTable", newTable);
        }

        public int GetPlayerNumber(string playerGuid)
        {
            return UserManager.Connections.ContainsValue(playerGuid) ?
                   UserManager.Connections.Where(c => c.Value == playerGuid).Select(c => c.Key).Min() : -1;
        }

        public Table AddPlayers(Table table)
        {
            if (UserManager.Connections.ContainsKey(1))
            {
                table.PlayerOneGuid = UserManager.Connections.Where(c => c.Key == 1).Select(c => c.Value).First();
            }
            if (UserManager.Connections.ContainsKey(2))
            {
                table.PlayerTwoGuid = UserManager.Connections.Where(c => c.Key == 2).Select(c => c.Value).First();
            }

            return table;
        }
    }
}
