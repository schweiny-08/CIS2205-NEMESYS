using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nemesys.Models;
using Nemesys.Models.FormModels;
using Microsoft.AspNetCore.Identity;
//using Nemesys.Models.UserModels;

namespace Nemesys.DAL
{
    public class DbInitializer
    {
        public static void Initialize(NemesysContext context)
        {
            context.Database.EnsureCreated();

            if (context.HazardTypes.Any())
                return;


            // Hazard Types

            var hazardTypes = new HazardType[] {
                new HazardType(){ 
                    hazardTypeName = "Slippery Hazard"
                },
                new HazardType(){ 
                    hazardTypeName = "Obstructive Hazard"
                },
                new HazardType(){ 
                    hazardTypeName = "Electrical Hazard"
                },
                new HazardType(){ 
                    hazardTypeName = "Fire Hazard"
                }
            };

            foreach (HazardType hazardType in hazardTypes) {
                context.HazardTypes.Add(hazardType);
            }
            context.SaveChanges();
        }
    }
}