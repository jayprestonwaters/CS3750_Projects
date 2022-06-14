package com.example.investmentproject.ticker.repositories;

import com.example.investmentproject.ticker.domain.Investment;
import org.springframework.data.repository.CrudRepository;

import java.util.List;

// This will be AUTO IMPLEMENTED by Spring into a Bean called investmentRepository
// CRUD refers Create, Read, Update, Delete

public interface InvestmentRepository extends CrudRepository<Investment, Integer> {
    List<Investment> findInvestmentByuserid (Integer userid);
}