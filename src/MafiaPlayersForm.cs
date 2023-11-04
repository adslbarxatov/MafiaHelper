using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает форму ввода списка игроков
	/// </summary>
	public partial class MafiaPlayersForm: Form
		{
		// Переменные и константы
		private char[] linesSplitters = new char[] { '\r', '\n' };
		private char[] rolesSplitters = new char[] { '\t', ' ' };
		private string rolesAliases;
		private string[] rolesNames;

		private const uint minPlayers = 6;
		private const uint maxPlayers = 20;

		/// <summary>
		/// Конструктор. Запускает главную форму
		/// </summary>
		public MafiaPlayersForm ()
			{
			// Инициализация
			InitializeComponent ();
			this.Text = ProgramDescription.AssemblyTitle;
			RDGenerics.LoadWindowDimensions (this);

			this.CancelButton = BExit;
			this.AcceptButton = BBegin;

			BExit.Text = Localization.GetDefaultText (LzDefaultTextValues.Button_Cancel);
			BBegin.Text = Localization.GetText ("BBegin");

			PlayersText.Text = RDGenerics.GetAppSettingsValue ("PlayersList");
			PlayersLabel.Text = string.Format (Localization.GetText ("PlayersText"), minPlayers);

			rolesAliases = Localization.GetText ("RolesAliases");
			rolesNames = Localization.GetText ("RolesNames").Split (linesSplitters,
				StringSplitOptions.RemoveEmptyEntries);

			for (int i = 0; i < rolesNames.Length; i++)
				PlayersLabel.Text += (Localization.RN + "• " + rolesAliases[i] +
					(i == 0 ? Localization.GetText ("RolesOPrefix") : "") + " – " + rolesNames[i].ToLower ());

			// Запуск
			this.ShowDialog ();
			}

		// Закрытие окна
		private void BExit_Click (object sender, EventArgs e)
			{
			RDGenerics.SetAppSettingsValue ("PlayersList", PlayersText.Text);
			this.Close ();
			}

		private void MafiaPlayersForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			RDGenerics.SaveWindowDimensions (this);
			}

		// Запуск
		private void BBegin_Click (object sender, EventArgs e)
			{
			// Определение числа игроков
			string[] pl = PlayersText.Text.Split (linesSplitters, StringSplitOptions.RemoveEmptyEntries);
			/*if (pl.Length < minPlayers)
				{
				RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "NotEnoughPlayersMessage");
				return;
				}*/

			// Счётчики
			uint[] counters = new uint[rolesAliases.Length];
			for (int i = 0; i < rolesAliases.Length; i++)
				counters[i] = 0;

			// Разбор
			for (int i = 0; i < pl.Length; i++)
				{
				string[] v = pl[i].Split (rolesSplitters, StringSplitOptions.RemoveEmptyEntries);

				// Такое может случиться при забое строки пробелами
				if (v.Length < 1)
					continue;

				// Обычный игрок
				if (v.Length == 1)
					{
					players.Add (new MafiaPlayer (v[0], MafiaPlayerRoles.Townspeople));
					counters[0]++;
					continue;
					}

				// Роли
				string c = v[1].ToUpper ().Substring (0, 1);
				if (!rolesAliases.Contains (c))
					{
					RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
						string.Format (Localization.GetText ("UnknownRoleMessage"), i + 1));
					players.Clear ();
					return;
					}

				int r = rolesAliases.IndexOf (c);
				players.Add (new MafiaPlayer (v[0], (MafiaPlayerRoles)r));
				counters[r]++;
				}

			// Защита
			if (players.Count < minPlayers)
				{
				RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "NotEnoughPlayersMessage");
				players.Clear ();
				return;
				}

			if (players.Count > maxPlayers)
				{
				RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
					string.Format (Localization.GetText ("TooMuchPlayersMessage"), maxPlayers));
				players.Clear ();
				return;
				}

			// Контроль ролей
			if ((counters[1] > players.Count / 2) || (counters[1] < 2))
				{
				RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "NotEnoughRolesMessage");
				players.Clear ();
				return;
				}

			if ((players.Count - counters[0] - counters[1]) > (rolesAliases.Length - 2))
				{
				RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "TooMuchSecondaryRolesMessage");
				players.Clear ();
				return;
				}

			// Успешно. Сохранение игроков без ролей
			string s = "";
			for (int i = 0; i < players.Count; i++)
				s += (players[i].Name + rolesSplitters[0].ToString () + Localization.RN);
			RDGenerics.SetAppSettingsValue ("PlayersList", s);

			this.Close ();
			}

		/// <summary>
		/// Возвращает список введённых игроков
		/// </summary>
		public List<MafiaPlayer> Players
			{
			get
				{
				return players;
				}
			}
		private List<MafiaPlayer> players = new List<MafiaPlayer> ();
		}
	}
