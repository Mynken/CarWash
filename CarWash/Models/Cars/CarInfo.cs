using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWash.Models.Cars
{
    public class CarInfo
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Wprowadź model samochodu")]
        public string Model { get; set; }

        [DisplayName("Kolor")]
        public string Color { get; set; }

        [DisplayName("Przyjęty o godzine")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalTime { get; set; }

        [DisplayName("Odebrany o godzine")]
        public DateTime? PickUpTime { get; set; }

        [DisplayName("Uslugi")]
        public string WashType { get; set; }

        [Required(ErrorMessage = "Wprowadź cenę")]
        [DisplayName("Cena")]
        public int Cost { get; set; }

        [DisplayName("Zaplacone?")]
        public bool PaidConfirmed { get; set; } = false;

        [DisplayName("Odebrane?")]
        public bool TakeConfirmed { get; set; } = false;

        [DisplayName("Typ samochodu")]
        public string TypeOfCar { get; set; }

        [DisplayName("Faktura")]
        public bool Faktura { get; set; } = false;
        public string Status { get; set; }

        [DisplayName("Płatność")]
        public string Payment { get; set; }        
    }
}