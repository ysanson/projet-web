//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VeterinaryClinicManagment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Remarques
    {
        public int idRemarques { get; set; }
        [Display(Name ="Date de la remarque")]
        public Nullable<System.DateTime> dateRemarque { get; set; }
        [Display(Name ="Contenu de la remarque")]
        public string contenuRemarque { get; set; }
        [Required]
        [Display(Name ="Animal")]
        public int IdAnimal { get; set; }
    
        public virtual Animal Animal { get; set; }
    }
}
