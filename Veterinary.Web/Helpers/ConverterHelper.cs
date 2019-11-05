﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinary.Common.Models;
using Veterinary.Web.Data.Entities;
using Veterinary.Web.Models;
using Veterinary.Web.Models.Data;

namespace Veterinary.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }
        public async Task<Pet> ToPetAsync(PetViewModel model, string path, bool isNew)
        {
            var pet = new Pet
            {
                Agendas = model.Agendas,
                Born = model.Born,
                Histories = model.Histories,
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Name = model.Name,
                Owner = await _dataContext.Owners.FindAsync(model.OwnerId),
                PetType = await _dataContext.PetTypes.FindAsync(model.PetTypeId),
                PetSex = await _dataContext.PetSexes.FindAsync(model.PetSexId),
                Race = model.Race,
                Remarks = model.Remarks,
            };

            return pet;
        }


        public PetViewModel ToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Agendas = pet.Agendas,
                Born = pet.Born,
                Histories = pet.Histories,
                ImageUrl = pet.ImageUrl,
                Name = pet.Name,
                Owner = pet.Owner,
                PetType = pet.PetType,
                Race = pet.Race,
                Remarks = pet.Remarks,
                Id = pet.Id,
                OwnerId = pet.Owner.Id,
                PetTypeId = pet.PetType.Id,
                PetSexId = pet.PetSex.Id,
                PetTypes = _combosHelper.GetComboPetTypes(),
                PetSexs = _combosHelper.GetComboPetSex()
            };

        }

        public async Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew)
        {

            //TODO: organizar fechas al momenton de ver y guardar una historia
            return new History
            {
                Date = model.Date.ToUniversalTime(),
                Description = model.Description,
                Id = isNew ? 0 : model.Id,
                Pet = await _dataContext.Pets.FindAsync(model.PetId),
                Remarks = model.Remarks,
                ServiceType = await _dataContext.ServiceTypes.FindAsync(model.ServiceTypeId)
            };

        }

        public HistoryViewModel ToHistoryViewModel(History history)
        {
            return new HistoryViewModel
            {
                Date = history.Date,
                Description = history.Description,
                Id = history.Id,
                PetId = history.Pet.Id,
                Remarks = history.Remarks,
                ServiceTypeId = history.ServiceType.Id,
                ServiceTypes = _combosHelper.GetComboServiceTypes()
            };

        }

        public PetResponse ToPetResponse(Pet pet)
        {
            if (pet == null)
            {
                return null;
            }

            return new PetResponse
            {
                Born = pet.Born,
                Id = pet.Id,
                ImageUrl = pet.ImageFullPath,
                Name = pet.Name,
                PetType = pet.PetType.Name,
                Race = pet.Race,
                Remarks = pet.Remarks
            };
        }

        public OwnerResponse ToOwnerResposne(Owner owner)
        {
            if (owner == null)
            {
                return null;
            }

            return new OwnerResponse
            {
                Address = owner.User.Address,
                Document = owner.User.Document,
                Email = owner.User.Email,
                FirstName = owner.User.FirstName,
                LastName = owner.User.LastName,
                PhoneNumber = owner.User.PhoneNumber
            };
        }

    }
}
