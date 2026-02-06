
//==========================================================
// Student Number : S10271091G
// Student Name : Capili Jiliana Sky Almonte
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10272091_PRG2Assignment
{
    internal class SpecialOffer
    {
        private string offerCode;
        private string offerName;
        private double discount;

        public string OfferCode;
        public string OfferName;
        public double Discount;

        public SpecialOffer() { }

        public SpecialOffer(string offerCode,string offerName,double discount)
        {
            OfferCode = offerCode;
            OfferName = offerName;
            Discount = discount;
        }

        public string ToString()
        {
            return $"{OfferCode,-10}{OfferName,-10}{Discount,-10}";
        }
    }
}
