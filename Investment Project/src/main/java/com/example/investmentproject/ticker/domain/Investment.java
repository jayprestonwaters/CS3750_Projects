package com.example.investmentproject.ticker.domain;

import java.math.BigDecimal;
import javax.persistence.*;
import java.util.Date;

@Entity
public class Investment {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer id;

    private Integer userid;

    private BigDecimal subdivision;

    private BigDecimal invested;

    private BigDecimal shares;

    private String ticker_symbol;

    private Date timestamp;

    private BigDecimal investmentPrice;

    public Investment() {}

    public Investment(Integer userid, BigDecimal subdivision,
                      BigDecimal invested, BigDecimal shares,
                      String ticker_symbol, Date timestamp,
                      BigDecimal investmentPrice) {
        this.userid = userid;
        this.subdivision = subdivision;
        this.invested = invested;
        this.shares = shares;
        this.ticker_symbol = ticker_symbol;
        this.timestamp = timestamp;
        this.investmentPrice = investmentPrice;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getUserid() {
        return userid;
    }

    public void setUserid(Integer userid) {
        this.userid = userid;
    }

    public BigDecimal getSubdivision() {
        return subdivision;
    }

    public void setSubdivision(BigDecimal subdivision) {
        this.subdivision = subdivision;
    }

    public BigDecimal getInvested() {
        return invested;
    }

    public void setInvested(BigDecimal invested) {
        this.invested = invested;
    }

    public BigDecimal getShares() {
        return shares;
    }

    public void setShares(BigDecimal shares) {
        this.shares = shares;
    }

    public String getTicker_Symbol() {
        return ticker_symbol;
    }

    public void setTicker_symbol(String ticker_symbol) {
        this.ticker_symbol = ticker_symbol;
    }

    public Date getTimestamp() {
        return timestamp;
    }

    public void setTimestamp(Date timestamp) {
        this.timestamp = timestamp;
    }

    public BigDecimal getinvestmentPrice() {
        return investmentPrice;
    }

    public void setinvestmentPrice(BigDecimal investmentPrice) {
        this.investmentPrice = investmentPrice;
    }
}
