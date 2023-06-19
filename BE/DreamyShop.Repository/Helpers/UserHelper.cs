using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Extensions;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos.User;
using DreamyShop.Domain.Shared.Types;

namespace DreamyShop.Repository.Helpers
{
    public static class UserHelper
    {
        /// <summary>
        /// Check phone exist or not in table User
        /// </summary>
        /// <param name="query"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmailExist(this IQueryable<User> query, string email)
        {
            if (String.IsNullOrEmpty(email)) return false;
            return query.Any(e => e.Email.Contains(email));
        }

        /// <summary>
        /// Check phone exist or not in table User
        /// </summary>
        /// <param name="query"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsPhoneExist(this IQueryable<User> query, string phone)
        {
            var result = phone.StandardPhone().GetLast9Digits();
            if (result.Length < 9) return false;
            return query.Any(e => e.Phone.Contains(result));
        }

        /// <summary>
        /// Standardized phone number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string StandardPhone(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return new ApiErrorResult<string>((int)ErrorCodes.CredentialsInvalid).Message;
            }
            return "+" + phone.Replace("+", "").RemoveAllWhiteSpace();
        }

        public static string GetLast9Digits(this string phone)
        {
            // Get everything after (phone.Length - 9) if (phone.Length - 9) > 0
            return phone.Substring(Math.Max(0, phone.Length - 9));
        }

        /// <summary>
        /// Get admin or customer
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static AuthEntity GetAuthEntity(this IQueryable<User> query)
        {
            var user = query.Select(u => new AuthEntity()
            {
                UserID = u.Id.ToString(),
                Email = u.Email,
                FullName = u.FullName,
                Avatar = u.Avatar,
                Phone = u.Phone,
                RoleTypes = u.Roles.Where(r => r.RoleType != (byte)RoleType.Customer).Select(r => r.RoleType).ToList()
            }).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }

}
