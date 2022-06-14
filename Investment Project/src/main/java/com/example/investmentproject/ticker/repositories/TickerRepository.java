package com.example.investmentproject.ticker.repositories;

import com.example.investmentproject.ticker.domain.Ticker;
import org.springframework.data.repository.CrudRepository;
import yahoofinance.Stock;

import java.util.Date;
import java.util.List;

// This will be AUTO IMPLEMENTED by Spring into a Bean called tickerRepository
// CRUD refers Create, Read, Update, Delete

public interface TickerRepository extends CrudRepository<Ticker, Long> {
}

