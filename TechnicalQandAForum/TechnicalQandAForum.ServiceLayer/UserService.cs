using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalQandAForum.DomainModels;
using TechnicalQandAForum.ViewModels;
using TechnicalQandAForum.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace TechnicalQandAForum.ServiceLayer
{
    public interface IUserService
    {
        int InsertUser(RegisterViewModel registerViewModel);
        void UpdateUserDetails(EditUserDetailsViewModel editUserDetailsViewModel);
        void UpdateUserpassword(EditUserPasswordViewModel editUserPasswordViewModel);
        void DeleteUser(int userId);
        List<UserViewModel> GetUsers();
        UserViewModel GetUserByEmailAndPassword(string email, string password);
        UserViewModel GetUserByEmail(string email);
        UserViewModel GetUserByUserId(int userId);
    }

    public class UserService : IUserService
    {
        IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public int InsertUser(RegisterViewModel registerViewModel)
        {
            var config = new MapperConfiguration(c => { c.CreateMap<RegisterViewModel, User>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<RegisterViewModel, User>(registerViewModel);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(registerViewModel.Password);
            userRepository.InsertUser(user);
            int uid = userRepository.GetLatestUserId();
            return uid;
        }

        public void UpdateUserDetails(EditUserDetailsViewModel editUserDetailsViewModel)
        {
            var config = new MapperConfiguration(c => { c.CreateMap<EditUserDetailsViewModel, User>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserDetailsViewModel, User>(editUserDetailsViewModel);
            userRepository.UpdateUserDetails(user);
        }

        public void UpdateUserpassword(EditUserPasswordViewModel editUserPasswordViewModel)
        {
            var config = new MapperConfiguration(c => { c.CreateMap<EditUserPasswordViewModel, User>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserPasswordViewModel, User>(editUserPasswordViewModel);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(editUserPasswordViewModel.Password);
            userRepository.UpdateUserpassword(user);
        }

        public void DeleteUser(int userId)
        {
            userRepository.DeleteUser(userId);
        }

        public List<UserViewModel> GetUsers()
        {
            List<User> users = userRepository.GetUsers();
            var config = new MapperConfiguration(c => { c.CreateMap<User, UserViewModel>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> userViewModels = mapper.Map<List<User>, List<UserViewModel>>(users);
            return userViewModels;
        }

        public UserViewModel GetUserByEmailAndPassword(string email, string password)
        {
            User user = userRepository.GetUserByEmailAndPassword(email,SHA256HashGenerator.GenerateHash(password)).FirstOrDefault();
            UserViewModel userViewModel = null;
            if (user != null)
            {
                var config = new MapperConfiguration(c => { c.CreateMap<User, UserViewModel>(); c.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                userViewModel = mapper.Map<User, UserViewModel>(user);
            }
            return userViewModel;
        }

        public UserViewModel GetUserByEmail(string email)
        {
            User user = userRepository.GetUserByEmail(email).FirstOrDefault();
            UserViewModel userViewModel = null;
            if (user != null)
            {
                var config = new MapperConfiguration(c => { c.CreateMap<User, UserViewModel>(); c.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                userViewModel = mapper.Map<User, UserViewModel>(user);
            }
            return userViewModel;
        }

        public UserViewModel GetUserByUserId(int userId)
        {
            User user = userRepository.GetUserByUserId(userId).FirstOrDefault();
            UserViewModel userViewModel = null;
            if (user != null)
            {
                var config = new MapperConfiguration(c => { c.CreateMap<User, UserViewModel>(); c.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                userViewModel = mapper.Map<User, UserViewModel>(user);
            }
            return userViewModel;
        }    
    }
}
