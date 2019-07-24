namespace Studio.Persistence.Context
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Studio.Domain.Entities;
    using Studio.Domain.Enumerations;
    using Studio.Domain.ValueObjects;

    public class StudioInitializer
    {
        public static void Initialize(StudioDbContext context)
        {
            var initializer = new StudioInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(StudioDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.StudioRoles.Any())
            {
                return; // Db has been seeded
            }

            this.SeedRoles(context);

            this.SeedCountries(context);

            this.SeedCities(context);

            this.SeedAddresses(context);

            this.SeedClients(context);

            this.SeedLocations(context);

            this.SeedEmployees(context);

            this.SeedIndustries(context);

            this.SeedServices(context);

            this.SeedLocationIndustries(context);

            this.SeedEmployeeServices(context);
        }

        private void SeedEmployeeServices(StudioDbContext context)
        {
            var employeeServices = new []
            {
                new EmployeeService { EmployeeId = 1, ServiceId = 1, Price = 8.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 1, ServiceId = 2, Price = 10.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 1, ServiceId = 3, Price = 20.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 2, ServiceId = 2, Price = 12.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 2, ServiceId = 4, Price = 40.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 3, ServiceId = 1, Price = 10.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 3, ServiceId = 2, Price = 12.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 4, ServiceId = 3, Price = 22.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 4, ServiceId = 4, Price = 45.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 5, ServiceId = 1, Price = 10.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 5, ServiceId = 2, Price = 12.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 6, ServiceId = 2, Price = 14.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 6, ServiceId = 4, Price = 50.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 7, ServiceId = 8, Price = 31.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 7, ServiceId = 9, Price = 21.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 8, ServiceId = 13, Price = 35.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 8, ServiceId = 14, Price = 40.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 9, ServiceId = 15, Price = 15.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 10, ServiceId = 15, Price = 15.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 11, ServiceId = 1, Price = 8.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 11, ServiceId = 2, Price = 15.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 12, ServiceId = 1, Price = 10.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 12, ServiceId = 2, Price = 18.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 13, ServiceId = 5, Price = 30.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 13, ServiceId = 6, Price = 12.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 14, ServiceId = 7, Price = 15.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 15, ServiceId = 15, Price = 18.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 16, ServiceId = 15, Price = 18.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 17, ServiceId = 8, Price = 35.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 17, ServiceId = 9, Price = 28.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 18, ServiceId = 10, Price = 150.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 18, ServiceId = 11, Price = 40.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 19, ServiceId = 9, Price = 25.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 19, ServiceId = 10, Price = 8.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 20, ServiceId = 12, Price = 35.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 20, ServiceId = 13, Price = 15.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 21, ServiceId = 15, Price = 22.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 22, ServiceId = 15, Price = 22.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 23, ServiceId = 1, Price = 8.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 24, ServiceId = 3, Price = 16.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 24, ServiceId = 2, Price = 10.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 24, ServiceId = 4, Price = 60.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 25, ServiceId = 1, Price = 8.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 25, ServiceId = 2, Price = 12.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 26, ServiceId = 1, Price = 8.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 26, ServiceId = 2, Price = 14.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 27, ServiceId = 12, Price = 35.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 27, ServiceId = 13, Price = 8.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 28, ServiceId = 12, Price = 32.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 28, ServiceId = 14, Price = 40.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 29, ServiceId = 1, Price = 8.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 29, ServiceId = 2, Price = 15.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 30, ServiceId = 8, Price = 26.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 30, ServiceId = 9, Price = 16.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 31, ServiceId = 10, Price = 100.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 31, ServiceId = 11, Price = 44.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 32, ServiceId = 8, Price = 25.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 32, ServiceId = 11, Price = 42.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 33, ServiceId = 1, Price = 6.00M, DurationInMinutes = "30" },
                new EmployeeService { EmployeeId = 34, ServiceId = 2, Price = 8.00M, DurationInMinutes = "30" }
            };

            context.EmployeeServices.AddRange(employeeServices);
            context.SaveChanges();
        }

        private void SeedLocationIndustries(StudioDbContext context)
        {
            var locationIndustries = new []
            {
                new LocationIndustry { LocationId = 1, IndustryId = 1, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 1, IndustryId = 2, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 2, IndustryId = 1, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 2, IndustryId = 2, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 3, IndustryId = 1, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 3, IndustryId = 2, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 4, IndustryId = 3, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 4, IndustryId = 4, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 5, IndustryId = 5, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 6, IndustryId = 1, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 7, IndustryId = 2, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 8, IndustryId = 5, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 9, IndustryId = 3, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 10, IndustryId = 3, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 10, IndustryId = 4, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 11, IndustryId = 5, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 12, IndustryId = 1, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 13, IndustryId = 1, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 14, IndustryId = 4, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new LocationIndustry { LocationId = 15, IndustryId = 3, Description = "Висококчествени услуга на супер цена!", CreatedOn = DateTime.UtcNow, IsDeleted = false }
            };

            context.LocationIndustries.AddRange(locationIndustries);
            context.SaveChanges();
        }

        private void SeedServices(StudioDbContext context)
        {
            var services = new []
            {
                new Service { Name = "Мъжко подстригване", IndustryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Дамско подстригване", IndustryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Боядисване на коса", IndustryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Бална прическа", IndustryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Педикюр", IndustryId = 2, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Оформяне на нокти", IndustryId = 2, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Лакиране", IndustryId = 2, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Пилинг", IndustryId = 3, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Кола маска", IndustryId = 3, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Лазерна епилация", IndustryId = 3, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Лечебна процедура за кожа", IndustryId = 3, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Спортен масаж", IndustryId = 4, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Целулитен масаж", IndustryId = 4, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Класически масаж", IndustryId = 4, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Service { Name = "Стандартна процедура", IndustryId = 5, CreatedOn = DateTime.UtcNow, IsDeleted = false }
            };

            context.Services.AddRange(services);
            context.SaveChanges();
        }

        private void SeedEmployees(StudioDbContext context)
        {
            var employees = new []
            {
                new Employee { FirstName = "Иван", LastName = "Петров", LocationId = 1, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Ивелина", LastName = "Сиракова", LocationId = 1, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Милен", LastName = "Димитров", LocationId = 2, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Мариета", LastName = "Димова", LocationId = 2, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Павел", LastName = "Георгиев", LocationId = 3, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Сиана", LastName = "Станиславова", LocationId = 3, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Калин", LastName = "Димитров", LocationId = 4, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Женя", LastName = "Симеонова", LocationId = 4, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Росен", LastName = "Желязков", LocationId = 5, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Татяна", LastName = "Николова", LocationId = 5, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Борис", LastName = "Трифонов", LocationId = 6, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Елисавета", LastName = "Бакалска", LocationId = 6, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Борислав", LastName = "Михайлов", LocationId = 7, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Стефка", LastName = "Костадинова", LocationId = 7, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Йордан", LastName = "Лечков", LocationId = 8, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Валя", LastName = "Балканска", LocationId = 8, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Григор", LastName = "Димитров", LocationId = 9, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Кремена", LastName = "Иванова", LocationId = 9, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Стефан", LastName = "Николов", LocationId = 10, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Никола", LastName = "Йорданов", LocationId = 10, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Петър", LastName = "Митев", LocationId = 11, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Асенка", LastName = "Митева", LocationId = 11, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Евлоги", LastName = "Караасенов", LocationId = 12, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Петилетка", LastName = "Петрова", LocationId = 12, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Пантелей", LastName = "Иванов", LocationId = 13, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Гица", LastName = "Красенова", LocationId = 13, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Колейман", LastName = "Шишманов", LocationId = 14, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Пена", LastName = "Дамнянова", LocationId = 14, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Коста", LastName = "Киряков", LocationId = 15, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Христина", LastName = "Асенова", LocationId = 15, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Ламби", LastName = "Костов", LocationId = 15, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Тихомир", LastName = "Трайков", LocationId = 15, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Николета", LastName = "Попова", LocationId = 1, CreatedOn = DateTime.Now, IsDeleted = false },
                new Employee { FirstName = "Анастасия", LastName = "Иванова", LocationId = 1, CreatedOn = DateTime.Now, IsDeleted = false }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        private void SeedIndustries(StudioDbContext context)
        {
            var industries = new []
            {
                new Industry { Name = "Коса", Possition = "Фризьор", CreatedOn = DateTime.Now, IsDeleted = false },
                new Industry { Name = "Нокти", Possition = "Маникюрист", CreatedOn = DateTime.Now, IsDeleted = false },
                new Industry { Name = "Тяло", Possition = "Козметик", CreatedOn = DateTime.Now, IsDeleted = false },
                new Industry { Name = "Масаж", Possition = "Масажист", CreatedOn = DateTime.Now, IsDeleted = false },
                new Industry { Name = "Тен", Possition = "Соларен специалист", CreatedOn = DateTime.Now, IsDeleted = false }
            };

            context.Industries.AddRange(industries);
            context.SaveChanges();
        }

        private void SeedLocations(StudioDbContext context)
        {
            var locations = new []
            {
                new Location { Name = "Студио \"Стайл\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 1, AddressId = 1 },
                new Location { Name = "Салон \"Бюти\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 1, AddressId = 2 },
                new Location { Name = "Салон за красота \"Колибри\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 1, AddressId = 3 },
                new Location { Name = "Естетичен център \"Здраве\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 2, AddressId = 4 },
                new Location { Name = "Студио \"ВИП\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 2, AddressId = 5 },
                new Location { Name = "Фризьорски салон \"Мода и стил\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 2, AddressId = 6 },
                new Location { Name = "Студио за маникюр \"Халена\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 3, AddressId = 7 },
                new Location { Name = "Солариум \"Тен\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 3, AddressId = 8 },
                new Location { Name = "Козметична клиника \"За теб\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 3, AddressId = 9 },
                new Location { Name = "Студио \"Енигма\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 4, AddressId = 10 },
                new Location { Name = "Студио \"Сонора\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 4, AddressId = 11 },
                new Location { Name = "Салон за красота \"Гили\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 4, AddressId = 12 },
                new Location { Name = "Фризьорски салон \"Ирен\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 5, AddressId = 13 },
                new Location { Name = "Масажно студио \"Релакс\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 5, AddressId = 14 },
                new Location { Name = "Козметичен салон \"Дийп\"",  IsOffice = false, Phone = "0885789456", Slogan = "Красотата е здраве.", Description = "Нашата визия за бизнеса е се крие в иновациите и доброто обслужване.", StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", CreatedOn = DateTime.UtcNow, IsDeleted = false, ClientId = 5, AddressId = 15 }
            };

            context.Locations.AddRange(locations);
            context.SaveChanges();
        }

        private void SeedClients(StudioDbContext context)
        {
            var clients = new []
            {
                new Client { CompanyName = "Стартъп ООД", VatNumber = "123456789", Manager = (Manager)"Иван Иванов", Phone = "0884123456", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Client { CompanyName = "Интернешенъл ЕООД", VatNumber = "123456789", Manager = (Manager)"Петър Петров", Phone = "0884123456", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Client { CompanyName = "Стайл Комерс ООД", VatNumber = "123456789", Manager = (Manager)"Герорги Георгиев", Phone = "0884123456", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Client { CompanyName = "Гуд Вижън ЕООД", VatNumber = "123456789", Manager = (Manager)"Михаил Михайлов", Phone = "0884123456", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Client { CompanyName = "Комерс ООД", VatNumber = "123456789", Manager = (Manager)"Захари Захариев", Phone = "0884123456", CreatedOn = DateTime.UtcNow, IsDeleted = false }
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();
        }

        private void SeedAddresses(StudioDbContext context)
        {
            var inputAddressesData = new [] 
            {
                new InputAddressData { Street = "Васил Левски", Number = "1" },
                new InputAddressData { Street = "Христо Ботев", Number = "2" },
                new InputAddressData { Street = "Георги Раковски", Number = "3" },
                new InputAddressData { Street = "Иван Вазов", Number = "4" },
                new InputAddressData { Street = "Хаджи Димитър", Number = "5" },
                new InputAddressData { Street = "Янтра", Number = "6" },
                new InputAddressData { Street = "Осъм", Number = "7" },
                new InputAddressData { Street = "Искър", Number = "8" },
                new InputAddressData { Street = "Тунджа", Number = "9" },
                new InputAddressData { Street = "Огоста", Number = "10" },
                new InputAddressData { Street = "Росица", Number = "11" },
                new InputAddressData { Street = "Марица", Number = "12" },
                new InputAddressData { Street = "Доналд Тръмп", Number = "13" },
                new InputAddressData { Street = "Владимир Путин", Number = "14" },
                new InputAddressData { Street = "Си Дзи Пин", Number = "15" },
                new InputAddressData { Street = "Ангела Меркел", Number = "16" },
                new InputAddressData { Street = "Оборище", Number = "17" },
                new InputAddressData { Street = "Тракия", Number = "18" },
                new InputAddressData { Street = "Дунав", Number = "19" },
                new InputAddressData { Street = "Ропотамо", Number = "20" }
            };

            var addresses = new []
            {
                new Address { AddressFormat = (AddressFormat) inputAddressesData[0], Longitude = 10.00M, Latitude = 10.00M, CityId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[1], Longitude = 20.00M, Latitude = 20.00M, CityId = 2, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[2], Longitude = 30.00M, Latitude = 30.00M, CityId = 3, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[3], Longitude = 40.00M, Latitude = 40.00M, CityId = 4, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[4], Longitude = 50.00M, Latitude = 50.00M, CityId = 5, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[5], Longitude = 60.00M, Latitude = 60.00M, CityId = 6, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[6], Longitude = 70.00M, Latitude = 70.00M, CityId = 7, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[7], Longitude = 80.00M, Latitude = 80.00M, CityId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[8], Longitude = 90.00M, Latitude = 90.00M, CityId = 2, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[9], Longitude = 10.10M, Latitude = 10.10M, CityId = 3, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[10], Longitude = 10.20M, Latitude = 10.20M, CityId = 4, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[11], Longitude = 10.30M, Latitude = 10.30M, CityId = 5, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[12], Longitude = 10.40M, Latitude = 10.40M, CityId = 6, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[13], Longitude = 10.50M, Latitude = 10.50M, CityId = 7, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[14], Longitude = 10.60M, Latitude = 10.60M, CityId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[15], Longitude = 10.70M, Latitude = 10.70M, CityId = 2, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[16], Longitude = 10.80M, Latitude = 10.80M, CityId = 3, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[17], Longitude = 10.90M, Latitude = 10.90M, CityId = 4, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[18], Longitude = 10.11M, Latitude = 10.11M, CityId = 5, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new Address { AddressFormat = (AddressFormat) inputAddressesData[19], Longitude = 10.12M, Latitude = 10.12M, CityId = 6, CreatedOn = DateTime.UtcNow, IsDeleted = false }
            };

            context.Addresses.AddRange(addresses);
            context.SaveChanges();
        }

        private void SeedCities(StudioDbContext context)
        {
            var cities = new []
            {
                new City { Name = "София", CountryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new City { Name = "Варна", CountryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new City { Name = "Бургас", CountryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new City { Name = "Пловдив", CountryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new City { Name = "Стара Загора", CountryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new City { Name = "Русе", CountryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new City { Name = "Велико Търново", CountryId = 1, CreatedOn = DateTime.UtcNow, IsDeleted = false }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }

        private void SeedCountries(StudioDbContext context)
        {
            var country = new Country { Name = "България" };

            context.Countries.Add(country);
            context.SaveChanges();
        }

        private void SeedRoles(StudioDbContext context)
        {
            var roles = new [] 
            {
                new StudioRole { Id = Guid.NewGuid().ToString(), Name = "Administrator", NormalizedName = "ADMINISTRATOR", CreatedOn = DateTime.UtcNow, IsDeleted = false },
                new StudioRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER", CreatedOn = DateTime.UtcNow, IsDeleted = false },
            };

            context.StudioRoles.AddRange(roles);
            context.SaveChanges();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}