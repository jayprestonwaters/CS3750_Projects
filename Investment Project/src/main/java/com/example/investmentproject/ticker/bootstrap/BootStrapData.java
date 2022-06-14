package com.example.investmentproject.ticker.bootstrap;

import com.example.investmentproject.ticker.domain.Ticker;
import com.example.investmentproject.ticker.repositories.TickerRepository;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;
import yahoofinance.Stock;
import yahoofinance.YahooFinance;

import java.io.IOException;

@Component
public class BootStrapData implements CommandLineRunner {

    private final TickerRepository tickerRepo;

    public BootStrapData(TickerRepository tickerRepo) {
        this.tickerRepo = tickerRepo;
    }

    @Override
    public void run(String... args) throws Exception {
        try {
            Stock apple = YahooFinance.get("AAPL");
            Stock microsoft = YahooFinance.get("MSFT");
            Stock tesla = YahooFinance.get("TSLA");

        } catch (IOException e) {
            throw new RuntimeException(e);
        }

    }
}
