using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minifutbol.Utils;

namespace Minifutbol.BL
{
  public class LogicResult<TResult> where TResult : class
  {
    public List<Error> ErrorList { get; set; } = new List<Error>();
    public bool IsSuccess => !ErrorList.Any();
    public int? TotalCount { get; set; }
    public TResult Output { get; set; }
  }
}