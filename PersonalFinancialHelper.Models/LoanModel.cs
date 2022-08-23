﻿namespace PersonalFinancialHelper.Models;

public class LoanModel
{
    protected int PurchasePrice { get; set; }
    protected int DownPayment { get; set; }
    protected int LoanAmount { get; set; }
    protected int LoanTerm { get; set; } //in months
    protected double AnnualAnnualInterestRate { get; set; }
    protected double MonthlyInterestRate { get; set; }
    protected List<double> RemainingPrinciple { get; set; } = new();
    protected List<double> TotalAmountPaid { get; set; } = new();
    protected List<double> TotalPrinciplePaid { get; set; } = new();
    protected List<double> TotalInterestPaid { get; set; } = new();
    protected int CurrentMonth { get; set; }

    protected LoanModel()
    {
        
    }
    
    public LoanModel(int purchasePrice, int downPayment, int loanTerm, double annualInterestRate)
    {
        PurchasePrice = purchasePrice;
        DownPayment = downPayment;
        LoanAmount = PurchasePrice - DownPayment;
        LoanTerm = loanTerm;
        AnnualAnnualInterestRate = annualInterestRate;
        MonthlyInterestRate = AnnualAnnualInterestRate / 12;
        
        RemainingPrinciple.Add(PurchasePrice - DownPayment);
        TotalAmountPaid.Add(DownPayment);
        TotalPrinciplePaid.Add(DownPayment);
        TotalInterestPaid.Add(0.0);
        
        RunModel();
    }

    public void Print()
    {
        for (var i = 0; i < LoanTerm; i++)
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine("Month " + i);
            Console.WriteLine("Total Principle Paid:\t" + TotalPrinciplePaid[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Interest Paid:\t" + TotalInterestPaid[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Amount Paid:\t" + TotalAmountPaid[i].ToString("$#,##0.00"));
        }
    }

    private void RunModel()
    {
        for (var i = 0; i < LoanTerm; i++)
        {
            TotalAmountPaid.Add(TotalAmountPaid[^1] + CalcMonthlyLoanPayment());
            TotalPrinciplePaid.Add(TotalPrinciplePaid[^1] + CalcMonthlyPrinciplePayment());
            TotalInterestPaid.Add(TotalInterestPaid[^1] + CalcMonthlyInterestPayment());
            RemainingPrinciple.Add(RemainingPrinciple[^1] - CalcMonthlyPrinciplePayment());
            CurrentMonth += 1;
        }
    }

    protected double CalcMonthlyInterestPayment()
    {
        return RemainingPrinciple[^1] * MonthlyInterestRate;
    }

    protected double CalcMonthlyPrinciplePayment()
    {
        return CalcMonthlyLoanPayment() - CalcMonthlyInterestPayment();
    }

    protected double CalcMonthlyLoanPayment()
    {
        return LoanAmount * MonthlyInterestRate * Math.Pow(1 + MonthlyInterestRate, LoanTerm) /
               (Math.Pow(1 + MonthlyInterestRate, LoanTerm) - 1);
    }
}