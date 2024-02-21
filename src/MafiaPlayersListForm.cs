using System;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает форму ручного ввода имён игроков
	/// </summary>
	public partial class MafiaPlayersListForm: Form
		{
		/// <summary>
		/// Конструктор. Запускает форму
		/// </summary>
		/// <param name="PlayersNamesList">Список имён игроков</param>
		public MafiaPlayersListForm (string PlayersNamesList)
			{
			// Инициализация
			InitializeComponent ();
			this.Text = ProgramDescription.AssemblyTitle;

			this.CancelButton = BExit;
			BExit.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Close);
			/*this.AcceptButton = BApply;*/
			BApply.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Apply);

			// Загрузка настроек
			RDGenerics.LoadWindowDimensions (this);
			PlayersNames.Text = PlayersNamesList;

			// Запуск
			this.ShowDialog ();
			}

		private void MafiaPlayersListForm_Shown (object sender, EventArgs e)
			{
			PlayersNames.DeselectAll ();
			PlayersNames.SelectionStart = PlayersNames.Text.Length;
			}

		// Закрытие окна
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void MafiaPlayersListForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			// Сохранение настроек
			RDGenerics.SaveWindowDimensions (this);
			}

		// Применение
		private void BApply_Click (object sender, EventArgs e)
			{
			cancelled = false;
			playersList = PlayersNames.Text;

			this.Close ();
			}

		/// <summary>
		/// Возвращает true, если сохранение было отменено
		/// </summary>
		public bool Cancelled
			{
			get
				{
				return cancelled;
				}
			}
		private bool cancelled = true;

		/// <summary>
		/// Возвращает список имён игроков
		/// </summary>
		public string PlayersList
			{
			get
				{
				return playersList;
				}
			}
		private string playersList = "";
		}
	}
