using RD_AAOW;
using System.Reflection;
using System.Resources;

// Управление общими сведениями о сборке
// ВИДИМЫЕ СТРОКИ
[assembly: AssemblyTitle (ProgramDescription.AssemblyDescription)]
[assembly: AssemblyCompany (RDGenerics.AssemblyCompany)]
// НЕВИДИМЫЕ СТРОКИ
[assembly: AssemblyDescription (ProgramDescription.AssemblyDescription)]
[assembly: AssemblyProduct (ProgramDescription.AssemblyTitle)]
[assembly: AssemblyCopyright (RDGenerics.AssemblyCopyright)]
[assembly: AssemblyVersion (ProgramDescription.AssemblyVersion)]

namespace RD_AAOW
	{
	/// <summary>
	/// Класс, содержащий сведения о программе
	/// </summary>
	public class ProgramDescription
		{
		/// <summary>
		/// Название программы
		/// </summary>
		public const string AssemblyTitle = AssemblyMainName + " v 2.1";

		/// <summary>
		/// Версия программы
		/// </summary>
		public const string AssemblyVersion = "2.1.0.0";

		/// <summary>
		/// Последнее обновление
		/// </summary>
		public const string AssemblyLastUpdate = "20.04.2025; 2:45";

		/// <summary>
		/// Пояснение к программе
		/// </summary>
		public const string AssemblyDescription = "Moderator’s helper for the game “Mafia”";

		/// <summary>
		/// Основное название сборки
		/// </summary>
		public const string AssemblyMainName = "MafiaHelper";

		/// <summary>
		/// Возвращает список менеджеров ресурсов для локализации приложения
		/// </summary>
		public readonly static ResourceManager[] AssemblyResources = [
			MafiaHelperResources.ResourceManager,

			MafiaHelper_ru_ru.ResourceManager,
			MafiaHelper_en_us.ResourceManager,
			];

		/// <summary>
		/// Возвращает набор ссылок на видеоматериалы по языкам
		/// </summary>
		public readonly static string[] AssemblyVideoLinks = [];

		/// <summary>
		/// Возвращает набор поддерживаемых языков
		/// </summary>
		public readonly static RDLanguages[] AssemblyLanguages = [
			RDLanguages.ru_ru,
			RDLanguages.en_us,
			];

		/// <summary>
		/// Возвращает описание сопоставлений файлов для приложения
		/// </summary>
		public readonly static string[][] AssemblyAssociations = [];
		}
	}
