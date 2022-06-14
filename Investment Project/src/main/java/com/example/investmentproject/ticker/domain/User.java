package com.example.investmentproject.ticker.domain;

import javax.persistence.Entity;
import javax.persistence.Id;
import java.math.BigDecimal;

@Entity // This tells Hibernate to make a table out of this class
public class User {
    @Id
//    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer id;

//    @OneToMany(mappedBy = "user_id")
//    private Set<Investment> investments = new HashSet<>();

    private BigDecimal starting_investment;

    public User() {}

    public User(BigDecimal starting_investment) {
        this.starting_investment = starting_investment;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public BigDecimal getStarting_investment() {
        return starting_investment;
    }

    public void setStarting_investment(BigDecimal starting_investment) {
        this.starting_investment = starting_investment;
    }
}