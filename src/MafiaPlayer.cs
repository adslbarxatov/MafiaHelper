namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает игрока игры
	/// </summary>
	public class MafiaPlayer
		{
		/// <summary>
		/// Возвращает имя игрока
		/// </summary>
		public string Name
			{
			get
				{
				return name;
				}
			}
		private string name;

		/// <summary>
		/// Возвращает роль игрока
		/// </summary>
		public MafiaPlayerRoles Role
			{
			get
				{
				return role;
				}
			}
		private MafiaPlayerRoles role;

		/// <summary>
		/// Возвращает true, если роль игрока – дневная
		/// </summary>
		public bool HasDayRole
			{
			get
				{
				return RoleIsDaytime (role);
				}
			}

		/// <summary>
		/// Метод возвращает true, если роль игрока – дневная
		/// </summary>
		public static bool RoleIsDaytime (MafiaPlayerRoles Role)
			{
			switch (Role)
				{
				case MafiaPlayerRoles.Townspeople:
				case MafiaPlayerRoles.Kamikaze:
					return true;

				default:
					return false;
				}
			}

		/// <summary>
		/// Возвращает true, если роль игрока относится к мафии
		/// </summary>
		public bool HasMafiaRole
			{
			get
				{
				return RoleIsMafia (role);
				}
			}

		/// <summary>
		/// Метод возвращает true, если роль игрока относится к мафии
		/// </summary>
		public static bool RoleIsMafia (MafiaPlayerRoles Role)
			{
			switch (Role)
				{
				case MafiaPlayerRoles.Mafia:
				case MafiaPlayerRoles.MafiaBoss:
					return true;

				default:
					return false;
				}
			}

		/// <summary>
		/// Возвращает true, если роль игрока относится к якудза
		/// </summary>
		public bool HasYakuzaRole
			{
			get
				{
				return RoleIsYakuza (role);
				}
			}

		/// <summary>
		/// Метод возвращает true, если роль игрока относится к якудза
		/// </summary>
		public static bool RoleIsYakuza (MafiaPlayerRoles Role)
			{
			switch (Role)
				{
				case MafiaPlayerRoles.Yakuza:
				case MafiaPlayerRoles.YakuzaBoss:
					return true;

				default:
					return false;
				}
			}

		/// <summary>
		/// Возвращает true, если роль игрока может быть применена сама на себя
		/// </summary>
		public bool RoleCanBeSelfApplied
			{
			get
				{
				return RoleIsSelfApplicable (role);
				}
			}

		/// <summary>
		/// Метод возвращает true, если роль игрока может быть применена сама на себя
		/// </summary>
		public static bool RoleIsSelfApplicable (MafiaPlayerRoles Role)
			{
			switch (Role)
				{
				case MafiaPlayerRoles.Townspeople:
				case MafiaPlayerRoles.Mafia:
				case MafiaPlayerRoles.Doctor:
				case MafiaPlayerRoles.Kamikaze:
				case MafiaPlayerRoles.Yakuza:
					return true;

				default:
					return false;
				}
			}

		/// <summary>
		/// Метод возвращает true, если роль игрока должна быть применена первой
		/// </summary>
		public static bool RoleShouldBeAppliedFirst (MafiaPlayerRoles Role)
			{
			switch (Role)
				{
				case MafiaPlayerRoles.Thief:
					return true;

				default:
					return false;
				}
			}

		/// <summary>
		/// Метод возвращает true, если роль не имеет собственного действия
		/// </summary>
		public static bool RoleHasNoAction (MafiaPlayerRoles Role)
			{
			switch (Role)
				{
				case MafiaPlayerRoles.MafiaBoss:
				case MafiaPlayerRoles.Immortal:
				case MafiaPlayerRoles.YakuzaBoss:
					return true;

				default:
					return false;
				}
			}

		/// <summary>
		/// Метод возвращает true, если роль может присутствовать в игре более
		/// чем в единственном экземпляре
		/// </summary>
		public static bool RoleCanBeTeam (MafiaPlayerRoles Role)
			{
			switch (Role)
				{
				case MafiaPlayerRoles.Mafia:
				case MafiaPlayerRoles.Yakuza:
				case MafiaPlayerRoles.Townspeople:
					return true;

				default:
					return false;
				}
			}

		/// <summary>
		/// Возвращает true, если игрок жив
		/// </summary>
		public bool Alive
			{
			get
				{
				return alive;
				}
			}
		private bool alive;

		/// <summary>
		/// Возвращает false, если игрок может голосовать и быть убитым
		/// </summary>
		public bool Muted
			{
			get
				{
				return muted;
				}
			}
		private bool muted;

		/// <summary>
		/// Возвращает true, если игрок может применять свою роль
		/// </summary>
		public bool RoleEnabled
			{
			get
				{
				return roleEnabled;
				}
			}
		private bool roleEnabled;

		/// <summary>
		/// Возвращает true, если роль игрока была раскрыта детективом
		/// </summary>
		public bool RoleWasRevealed
			{
			get
				{
				return roleWasRevealed;
				}
			}
		private bool roleWasRevealed;

		/// <summary>
		/// Возвращает true, если к игроку применено лечение
		/// </summary>
		public bool HealthLocked
			{
			get
				{
				return healthLocked;
				}
			}
		private bool healthLocked;

		/// <summary>
		/// Возвращает true, если игрок будет защищён на ближайшем голосовании
		/// </summary>
		public bool Protected
			{
			get
				{
				return isprotected;
				}
			}
		private bool isprotected;

		/// <summary>
		/// Метод переводит игрока в состояние «убит»
		/// </summary>
		public void Kill ()
			{
			alive = false;
			}

		/// <summary>
		/// Метод переводит игрока в состояние «жив»
		/// </summary>
		public void Heal ()
			{
			alive = healthLocked = true;
			}

		/// <summary>
		/// Метод переводит игрока в состояние «немой», если он жив
		/// </summary>
		public void Mute ()
			{
			muted = true;
			}

		/// <summary>
		/// Метод выводит игрока из состояний «немой» и «защищён», если он жив
		/// </summary>
		public void UnmuteUnprotect ()
			{
			muted = false;
			isprotected = false;
			}

		/// <summary>
		/// Метод переводит игрока в состояние «не может применять роль», если он жив
		/// </summary>
		public void DisableRole ()
			{
			roleEnabled = false;
			}

		/// <summary>
		/// Метод выводит игрока из состояния «не может применять роль», если он жив
		/// </summary>
		public void EnableRole ()
			{
			roleEnabled = true;
			}

		/// <summary>
		/// Метод фиксирует раскрытие роли игрока детективом
		/// </summary>
		public void RevealRole ()
			{
			roleWasRevealed = true;
			}

		/// <summary>
		/// Метод снимает состояние «вылечен» 
		/// </summary>
		public void UnlockHealth ()
			{
			healthLocked = false;
			}

		/// <summary>
		/// Метод переводит игрока в состояние «защищён», если он жив
		/// </summary>
		public void Protect ()
			{
			isprotected = true;
			}

		/// <summary>
		/// Метод переводит игрока в роль шерифа при определённых условиях
		/// </summary>
		public void TurnIntoSheriff ()
			{
			if (role == MafiaPlayerRoles.Detective)
				role = MafiaPlayerRoles.Sheriff;
			}

		/// <summary>
		/// Конструктор. Создаёт живого игрока
		/// </summary>
		/// <param name="PlayerName">Имя игрока</param>
		/// <param name="PlayerRole">Роль игрока</param>
		public MafiaPlayer (string PlayerName, MafiaPlayerRoles PlayerRole)
			{
			name = PlayerName;
			role = PlayerRole;

			alive = true;
			muted = false;
			roleEnabled = true;
			roleWasRevealed = false;
			healthLocked = false;
			isprotected = false;
			}
		}
	}
