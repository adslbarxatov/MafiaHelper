using System;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает точку входа приложения
	/// </summary>
	public static class MafiaHelperProgram
		{
		/// <summary>
		/// Главная точка входа для приложения
		/// </summary>
		[STAThread]
		public static void Main ()
			{
			// Инициализация
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);

			// Язык интерфейса и контроль XPUN
			if (!RDLocale.IsXPUNClassAcceptable)
				return;

			// Проверка запуска единственной копии
			if (!RDGenerics.IsAppInstanceUnique (true))
				return;

			// Контроль прав
			if (!RDGenerics.AppHasAccessRights (true, true))
				return;

			// Отображение справки и запроса на принятие Политики
			if (!RDInterface.AcceptEULA ())
				return;
			RDInterface.ShowAbout (true);

			// Запуск
			Application.Run (new MafiaHelperForm ());
			}
		}
	}
