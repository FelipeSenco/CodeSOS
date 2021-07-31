using System;
using System.Collections.Generic;
using System.Linq;
using CodeSOSProject.DomainModels;
using CodeSOSProject.ViewModels;
using CodeSOSProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace CodeSOSProject.ServiceLayer
{
    public interface IUserService
    {
        int InsertUser(RegisterViewModel uvm);
        void UpdateUserDetails(EditUserDetailsViewModel eudvm);
        void UpdateUserPassword(EditUserPasswordViewModel eupvm);
        void DeleteUser(int UserID);
        List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPassword(string Email, string Password);
        UserViewModel GetUsersByEmail(string Email);
        UserViewModel GetUsersByUserID(int UserID);
    }

    public class UsersService : IUserService
    {
        IUsersRepository usersRepository;

        public UsersService()
        {
            usersRepository = new UsersRepository();
        }

        public int InsertUser(RegisterViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); } );
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<RegisterViewModel, User>(uvm);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password);
            usersRepository.InsertUser(user);
            int UserID = usersRepository.GetLatestUserID();
            return UserID;
        }

        public void UpdateUserDetails(EditUserDetailsViewModel eudvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserDetailsViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserDetailsViewModel, User>(eudvm);
            usersRepository.UpdateUserDetails(user);
        }

        public void UpdateUserPassword(EditUserPasswordViewModel eupvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserPasswordViewModel, User>(eupvm);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(eupvm.Password);
            usersRepository.UpdateUserPassword(user);
        }

        public void DeleteUser(int UserID)
        {
            usersRepository.DeleteUser(UserID);
        }

        public List<UserViewModel> GetUsers()
        {
            List<User> users = usersRepository.GetUsers();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> uvm = mapper.Map<List<User>, List<UserViewModel>>(users);
            return uvm;
        }

        public UserViewModel GetUsersByEmailAndPassword(string Email, string Password)
        {
            string PasswordHash = SHA256HashGenerator.GenerateHash(Password);
            User user = usersRepository.GetUsersByEmailAndPassword(Email, PasswordHash).FirstOrDefault();
            UserViewModel uvm = null;

            if (user != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(user);                
            }
            return uvm;
        }

        public UserViewModel GetUsersByEmail(string Email)
        {            
            User user = usersRepository.GetUsersByEmail(Email).FirstOrDefault();
            UserViewModel uvm = null;

            if (user != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(user);
            }
            return uvm;
        }

        public UserViewModel GetUsersByUserID(int UserID)
        {
            User user = usersRepository.GetUsersByUserID(UserID).FirstOrDefault();
            UserViewModel uvm = null;

            if (user != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(user);
            }
            return uvm;
        }
    }
}
