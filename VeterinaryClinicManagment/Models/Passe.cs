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

    public partial class Passe
    {
        [Display(Name ="Date de l'opération")]
        public Nullable<System.DateTime> DateOp { get; set; }
        [Display(Name ="Compte rendu de l'opération")]
        public string CompteRenduOP { get; set; }
        [Display(Name ="Animal")]
        public int IdAnimal { get; set; }
        [Display(Name ="Opération")]
        public int IdOperation { get; set; }
        [Display(Name ="Vétérinaire")]
        public int IdUtilisateur { get; set; }
    
        public virtual Animal Animal { get; set; }
        public virtual Operation Operation { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
    }
}