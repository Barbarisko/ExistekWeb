using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelsNew
{
	public abstract class BaseEntity : IBaseEntity
	{
		private int _id;

		[Key]
		[Required]
		public int Id { get => _id; set => _id = value; }
	}
}
