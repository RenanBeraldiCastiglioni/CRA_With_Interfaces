using System;
using CRA_With_Interfaces.Entities;

namespace CRA_With_Interfaces.Services
{
    class RentalService
    {
        public double PricePerHour { get; private set; }
        public double PricePerDay { get; private set; }

        //ESSE PROCESSO E CHAMADO DE INVERSAO DE CONTROLE POR MEIO INJECAO DE DEPENDENCIA
        private ITaxService _taxService;

        public RentalService(double pricePerHour, double pricePerDay, ITaxService taxService)
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
            _taxService = taxService;
        }

        public void ProcessInvoice(CarRental carRental)
        {
            TimeSpan duration = carRental.Finish.Subtract(carRental.Start);
            double basicPayment = 0.0;
            if (duration.TotalHours <= 12.0)
            {
                basicPayment = PricePerHour * Math.Ceiling(duration.TotalHours);
            }
            else
            {
                basicPayment = PricePerDay * Math.Ceiling(duration.TotalDays);
            }

            double Tax = _taxService.Tax(basicPayment);

            carRental.Invoice = new Invoice(basicPayment, Tax);
        }
    }
}