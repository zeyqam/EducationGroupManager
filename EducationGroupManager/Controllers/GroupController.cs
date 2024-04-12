﻿using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationGroupManager.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;

        public GroupController()
        {
            _groupService = new GroupService();

        }

        public async Task GroupCreate()
        {
            string name;
            bool isValidName = false;
            do
            {
                Console.WriteLine("Qrup adını daxil edin:");
                name = Console.ReadLine();
                isValidName = IsNameValid(name);
            } while (!isValidName);

            int capacity;
            bool isValidCapacity = false;
            do
            {
                Console.WriteLine("Qrupun kapasitesini daxil edin:");
                string capacityInput = Console.ReadLine();
                isValidCapacity = int.TryParse(capacityInput, out capacity) && capacity > 0;
                if (!isValidCapacity)
                {
                    Console.WriteLine("Xahiş edirik, düzgün bir kapasite daxil edin.");
                }
            } while (!isValidCapacity);

            int educationId;
            bool educationExists = false;
            do
            {
                Console.WriteLine("Təhsil İD-sini daxil edin:");
                string educationIdInput = Console.ReadLine();
                if (int.TryParse(educationIdInput, out educationId))
                {
                    educationExists = await _groupService.CheckIfEducationExistsAsync(educationId);
                    if (!educationExists)
                    {
                        Console.WriteLine("Xəta: Daxil edilmiş təhsil İD-si mövcud deyil. Əvvəlcə təhsil yaradın.");
                    }
                }
                else
                {
                    Console.WriteLine("Düzgün Təhsil İD formatı daxil edilməyib. Xahiş edirik yenidən daxil edin:");
                }
            } while (!educationExists);


            await _groupService.CreateGroupAsync(name, capacity, educationId);
            ConsoleColor.Green.WriteConsole("Qrup uğurla yaradıldı.");
        }

        private bool IsNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Qrup adı boş ola bilməz.");
                return false;
            }

            return true;
        }
    }




}