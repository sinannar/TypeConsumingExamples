using System;
using System.Collections.Generic;
using System.Text;

namespace TypeConsumingExamples
{
    //example of user defined conversions, conversion operator
    class Money
    {
        public Money(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; set; }

        public static implicit operator decimal(Money money)
        {
            return money.Amount;
        }

        public static implicit operator int(Money money)
        {
            return (int)money.Amount;
        }
    }
}
