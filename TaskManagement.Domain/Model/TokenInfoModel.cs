using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Model;
public record TokenInfoModel(string AccessToken, int AccessTokenExpireInMinutes);
