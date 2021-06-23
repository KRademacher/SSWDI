using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Management.ViewModels
{
    public class TreatmentViewModel
    {
        public Animal Animal { get; set; }
        public Treatment Treatment { get; set; }
        public List<Treatment> Treatments { get; set; }
        public string PerformedBy { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime PerformDate 
        { 
            get 
            {
                return _performedDate ?? DateTime.Now;
            }
            set
            { 
                _performedDate = value; 
            } 
        }

        private DateTime? _performedDate = null;
    }
}