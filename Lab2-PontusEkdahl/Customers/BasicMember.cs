﻿namespace Lab2_PontusEkdahl.Customers;

public class BasicMember : Customer
{
    public BasicMember(string name, string password) : base(name, password)
    {
        Discount = 0;
    }
}