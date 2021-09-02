using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltalizaApi.Models;

namespace AltalizaApi.Application
{
    public class CharacterApplication
    {
        private ApiContext _context;

        public CharacterApplication(ApiContext context)
        {
            _context = context;
        }

        public Character GetCharacterById(int id)
        {
            try {
                var character = _context.Character.Single(x => x.Id == id);
                return character;
            } catch (Exception) {
                return null;
            }
        }

        public string InsertCharacter(Character character)
        {
            try {
                if (character != null) {
                    _context.Add(character);
                    _context.SaveChanges();
                    return "Character added successfully";
                } else {
                    return "Character null";
                }
            } catch (Exception) {
                return "Error on adding character";
            }
        }

        public string InsertRental(Rental rental, int price)
        {
            try {
                if (rental != null) {
                    var vehicle = _context.Vehicle.Single(x => x.Id == rental.IdVeiculo);
                    if (vehicle.Estoque < 1) {
                        return "Vehicle not available";
                    }

                    _context.Add(rental);

                    var character = _context.Character.Single(x => x.Id == rental.IdPersonagem);
                    character.Carteira -= price;
                    vehicle.Estoque -= 1;
                    _context.SaveChanges();

                    return "Rental added successfully";
                } else {
                    return "Rental null";
                }
            } catch (Exception) {
                return "Error on adding rental";
            }
        }

        public string RenewRental(RenewRentalRequest renewRequest, int characterId, int rentalId)
        {
            try {
                var rental = _context.Rental.Single(x => x.Id == rentalId);
                    if (rental == null) {
                        return "Rental not found";
                    }
                    rental.DataExpiracao = rental.DataExpiracao.AddDays(renewRequest.Dias);

                    var character = _context.Character.Single(x => x.Id == characterId);
                    if (character == null) {
                        return "Character not found";
                    }
                    character.Carteira -= renewRequest.Preco;

                    _context.SaveChanges();

                    return "Rental renewed successfully";
            } catch (Exception) {
                return "Error on renewing rental";
            }
        }

        public string CancelRental(int characterId, int rentalId)
        {
            try {
                var rental = _context.Rental.Single(x => x.Id == rentalId);
                var vehicle = _context.Vehicle.Single(x => x.Id == rental.IdVeiculo);

                _context.Rental.Remove(rental);
                vehicle.Estoque += 1;

                _context.SaveChanges();

                return "Rental canceled successfully";
            } catch (Exception) {
                return "Error on canceling rental";
            }
        }

        public List<UserVehicle> GetAllUserVehicles(int characterId)
        {
            List<UserVehicle> userVehicleList = new List<UserVehicle>();
        
            try
            {
                userVehicleList = _context.Rental.Where(x => x.IdPersonagem == characterId).Join(
                    _context.Vehicle,
                    rental => rental.IdVeiculo,
                    vehicle => vehicle.Id,
                    (rental, vehicle) => new UserVehicle {
                        RentalId = rental.Id,
                        DataExpiracao = rental.DataExpiracao,
                        Nome = vehicle.Nome,
                        Imagem = vehicle.Imagem,
                        Preco1Dia = vehicle.Preco1Dia,
                        Preco7Dia = vehicle.Preco7Dia,
                        Preco15Dia = vehicle.Preco15Dia,
                        Disponivel = vehicle.Estoque > 0
                    }
                ).ToList();

                if (userVehicleList != null)
                {
                    return userVehicleList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
