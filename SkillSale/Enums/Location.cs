using System.ComponentModel.DataAnnotations;

namespace SkillSale.Enums
{
	public enum Location
	{
		[Display(Name = "Иссык-Куль")]
		IssykKul,
		[Display(Name = "Чуй")]
		Chuy,
		[Display(Name = "Талас")]
		Talas,
		[Display(Name = "Бишкек")]
		Bishkek,
		[Display(Name = "Ош")]
		Osh,
		[Display(Name = "Нарын")]
		Naryn,
		[Display(Name = "Джалал-Абад")]
		JalalAbad,
		[Display(Name = "Баткен")]
		Batken,
	}
}
