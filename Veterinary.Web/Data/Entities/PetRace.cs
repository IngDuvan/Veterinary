﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinary.Web.Data.Entities
{
    [Table("TblPetRace")]
    public class PetRace
    {
        public int Id { get; set; }

        [Display(Name = "Pet Race")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
