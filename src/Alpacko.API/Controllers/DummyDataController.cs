using Alpacko.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpacko.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyDataController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AlpackoDatabaseContext _context;

        public DummyDataController(IConfiguration configuration,
                               AlpackoDatabaseContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DummyData()
        {
            (int adminRoleId, int userRoleId) = await CreateUserRoles();
            int postOfficeId = await CreatePostOffice();
            await CreateUsers(adminRoleId, userRoleId, postOfficeId);

            int packageSizeNameId = await CreatePackageSizeName();
            int packageDetailId = await CreatePackageDetail(packageSizeNameId);
            int packageRecipientId = await CreatePackageRecipient();
            int packageSenderId = await CreatePackageSender();

            int nPackagesNotRegistered = _context.Package.Count(package => _context.RegisteredPackage.FirstOrDefault(x => x.PackageId == package.Id) != null);
            if (_context.Package.Count() == 0 || nPackagesNotRegistered < 10)
            {
                for (int i = 0; i < nPackagesNotRegistered - 10; i++)
                {
                    await CreatePackage(packageSenderId, packageRecipientId, packageDetailId);
                }

                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        private async Task CreatePackage(int senderId, int recipientId, int packageDetailId)
        {
            string id = KeyGenerator.GetUniqueKey(10, "1234567890".ToCharArray());
            Package package = new Package()
            {
                Id = id,
                SenderId = senderId,
                RecipientId = recipientId,
                PackageDetailId = packageDetailId
            };

            await _context.Package.AddAsync(package);
            await _context.SaveChangesAsync();
        }

        private async Task<int> CreatePackageSender()
        {
            int packageSenderId = -1;

            PackageSender packageSender = _context.PackageSender.FirstOrDefault(x => x.Email == "sender@alpacko.com");
            if (packageSender is null)
            {
                PackageSender sender = new PackageSender()
                {
                    FirstName = "Sender 1",
                    LastName = "Senderson",
                    Address = "Sender Street",
                    ZipCode = "12345",
                    City = "Sender",
                    Email = "sender@alpacko.com"
                };

                await _context.PackageSender.AddAsync(sender);
                await _context.SaveChangesAsync();
                packageSenderId = _context.PackageSender.FirstOrDefault(x => x.Email == "sender@alpacko.com").Id;
            }
            else
                packageSenderId = packageSender.Id;

            return packageSenderId;
        }

        private async Task<int> CreatePackageRecipient()
        {
            int packageRecipientId = -1;

            PackageRecipient packageRecipient = _context.PackageRecipient.FirstOrDefault(x => x.Email == "recipient@alpacko.com");
            if (packageRecipient is null)
            {
                PackageRecipient recipient = new PackageRecipient()
                {
                    FirstName = "Recipient 1",
                    LastName = "Recipientson",
                    Address = "Recipient Street",
                    ZipCode = "12345",
                    City = "Recipient",
                    Email = "recipient@alpacko.com"
                };

                await _context.PackageRecipient.AddAsync(recipient);
                await _context.SaveChangesAsync();
                packageRecipientId = _context.PackageRecipient.FirstOrDefault(x => x.Email == "recipient@alpacko.com").Id;
            }
            else
                packageRecipientId = packageRecipient.Id;

            return packageRecipientId;
        }

        private async Task<int> CreatePackageDetail(int packageSizeNameId)
        {
            int packageDetailId = -1;

            PackageDetail packageDetail = _context.PackageDetail.FirstOrDefault(x => x.SizeNameId == packageSizeNameId);
            if (packageDetail is null)
            {
                PackageDetail detail = new PackageDetail()
                {
                    SizeNameId = packageSizeNameId,
                    Price = 100,
                    MinLenght = 15,
                    MaxLenght = 100,
                    MinWidth = 15,
                    MaxWidth = 100,
                    MinHeight = 15,
                    MaxHeight = 100,
                    MinWeight = 500,
                    MaxWeight = 5000
                };

                await _context.PackageDetail.AddAsync(detail);
                await _context.SaveChangesAsync();
                packageDetailId = _context.PackageDetail.FirstOrDefault(x => x.SizeNameId == packageSizeNameId).Id;
            }
            else
                packageDetailId = packageDetail.Id;

            return packageDetailId;
        }

        private async Task<int> CreatePackageSizeName()
        {
            int packageSizeNameId = -1;

            PackageSizeName package = _context.PackageSizeName.FirstOrDefault(x => x.Name == "Alpacko Size");
            if (package is null)
            {
                await _context.PackageSizeName.AddAsync(new PackageSizeName() { Name = "Alpacko Size", Description = "Very big" });
                await _context.SaveChangesAsync();
                packageSizeNameId = _context.PackageSizeName.FirstOrDefault(x => x.Name == "Alpacko Size").Id;
            }
            else
                packageSizeNameId = package.Id;

            return packageSizeNameId;
        }

        private async Task CreateUsers(int adminRoleId, int userRoleId, int postOfficeId)
        {
            // Password = password
            User userAsAdmin = new User()
            {
                FirstName = "Admin",
                LastName = "Alpacko",
                Email = "admin@alpacko.com",
                Password = "0d6a6ad1505025c3fbbeb8dfd1991d5c7107e9ec5d4f57eb03c1cce84d7260c0",
                Salt = "8552064524189236949650075604769707570061978013768761634138257264",
                PostOfficeId = postOfficeId,
                UserRoleId = adminRoleId
            };
            // Password = password
            User userAsUser = new User()
            {
                FirstName = "User",
                LastName = "Alpacko",
                Email = "user@alpacko.com",
                Password = "5328785b0df74ce66853bfed021754e9e137db646076c9cb89adbd7c62065249",
                Salt = "2024867011993898042732015696229476257507987217872847848591343122",
                UserRoleId = userRoleId
            };

            if (_context.User.FirstOrDefault(x => x.Email == "admin@alpacko.com") is null)
                await _context.User.AddAsync(userAsAdmin);
            if (_context.User.FirstOrDefault(x => x.Email == "user@alpacko.com") is null)
                await _context.User.AddAsync(userAsUser);

            await _context.SaveChangesAsync();
        }

        private async Task<(int, int)> CreateUserRoles()
        {
            int adminRoleId = -1;
            int userRoleId = -1;

            UserRole admin = _context.UserRole.FirstOrDefault(x => x.Name == "Admin");
            if (admin is null)
            {
                await _context.UserRole.AddAsync(new UserRole() { Name = "Admin" });
                await _context.SaveChangesAsync();
                adminRoleId = _context.UserRole.FirstOrDefault(x => x.Name == "Admin").Id;
            }
            else
                adminRoleId = admin.Id;

            UserRole user = _context.UserRole.FirstOrDefault(x => x.Name == "User");
            if (user is null)
            {
                await _context.UserRole.AddAsync(new UserRole() { Name = "User" });
                await _context.SaveChangesAsync();
                userRoleId = _context.UserRole.FirstOrDefault(x => x.Name == "User").Id;
            }
            else
                userRoleId = user.Id;

            return (adminRoleId, userRoleId);
        }

        private async Task<int> CreatePostOffice()
        {
            int postOfficeId = -1;

            PostOffice alpackoOffice = _context.PostOffice.FirstOrDefault(x => x.Email == "alpacko@alpacko.com");
            if (alpackoOffice is null)
            {
                PostOffice postOffice = new PostOffice()
                {
                    Name = "Alpacko",
                    Address = "Alpacko Street",
                    ZipCode = "12345",
                    City = "Alpacko",
                    Email = "alpacko@alpacko.com"
                };

                await _context.PostOffice.AddAsync(postOffice);
                await _context.SaveChangesAsync();
                postOfficeId = _context.PostOffice.FirstOrDefault(x => x.Email == "alpacko@alpacko.com").Id;
            }
            else
                postOfficeId = alpackoOffice.Id;

            return postOfficeId;
        }
    }
}
