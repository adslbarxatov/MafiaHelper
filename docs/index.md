# Mafia helper: user guide
> **ƒ** &nbsp;RD AAOW FDL; 12.11.2023; 4:17



### Page contents

- [General information](#general-information)
- [Implemented game rules](#implemented-game-rules)
- [Implemented roles](#implemented-roles)
- [Features of the application](#features-of-the-application)
- [Download links](https://adslbarxatov.github.io/DPArray#mafia-helper)
- [Русская версия](https://adslbarxatov.github.io/MafiaHelper/ru)

---

### General information

***MafiaHelper*** is the moderator’s assistant for the game “Mafia”. It allows you to track
the current statuses of players, their roles, and also process their use according to the rules
of the game.

***Since there are many variants of “Mafia”, this guide will outline the rules that are included
in the current version of the application. They may not comply with official competition requirements
and may be changed or supplemented in new versions of the product.***

> Current version of the guide: MafiaHelper v 1.3

&nbsp;



### Implemented game rules

The game can be played by 6 to 20 people inclusive. There is also a person moderating the game and using
this application.

<img src="/MafiaHelper/img/01_en.png" />

When you launch the app, similar to the game itself, you’re prompted to specify the names of players
and their roles. In reality, this is done using simple playing cards or special thematic cards. They’re
distributed to players “in private”. Everyone (except the moderator) gets one role, which initially
only he / she knows.

Next, ***zero night*** is announced, and all players close their eyes (usually with their hands or masks,
options are possible). The moderator takes turns interviewing the roles present in the game (except for
`townspeople`) in order to “get to know” the players who got them. The information received is entered
into the application.

<img src="/MafiaHelper/img/02_en.png" />

Then a ***zero day*** is announced (the app is not used), during which players implement representative
functions related to the game process. The players see each other, which requires each of them to carefully
simulate the absence of any special role.

> The essence of the gameplay is the confrontation between civilians and the mafia. Each team strives
> to destroy the enemy. The psychological emphasis is on the confrontation between the organized minority
> (there are initially fewer mafia in the game, but players with this role recognize each other when they
> first meet and act together) and the unorganized majority (there are more civilians, but they don’t know
> about the roles of other players). Further actions are based on this.

The ***first night*** is announced (players close their eyes). The presenter takes turns interviewing
the roles (except for `townspeople`). The respective players open their eyes and point to some other
player (in some cases you can point to themselves or skip a turn) to whom they want to apply the action
of their role. The moderator makes appropriate notes in the app. Having completed the role, the player
closes his eyes.

<img src="/MafiaHelper/img/03_en.png" />

After interviewing all the roles, the moderator announces the ***first day***. When this action is performed,
the app processes the specified notes and displays the changed statuses of players.

<img src="/MafiaHelper/img/04_en.png" />

<img src="/MafiaHelper/img/05_en.png" />

<img src="/MafiaHelper/img/06_en.png" />

The moderator announces the changes that have occurred. Players use this information to discuss and make
assumptions about who the mafia is and how other roles are distributed. Naturally, players can pretend
and mislead each other in order to last longer in the game.

At the end of the day, a vote is held, based on the results of which one of the players is “executed”
(he leaves the game). A corresponding note is made in the app. As a rule, a certain time is allocated
for voting and discussion. If after its expiration the decision is not made, the “execution” is cancelled.

This is followed again by a night, during which actions similar to those described for the first night
are performed, but with a changed composition of players. The cycle of day and night phases continues
until a winning situation is achieved. For the ***mafia***, a victory is considered to be the state
when there are more players with this role than all the others combined (loss becomes mathematically
impossible). Victory for ***civilians*** will only be the complete destruction of the mafia.

<img src="/MafiaHelper/img/07_en.png" />

<img src="/MafiaHelper/img/08_en.png" />

&nbsp;



### Implemented roles

In the current version, each role in the game is associated with one available action. Additionally,
some roles have certain "abilities".

***Townspeople*** – an ordinary resident of the city, the main composition of the game. He always sleeps
at night. During the day, by voting together with the rest of the “living” players, he can choose
one player and “execute” him (such a player leaves the game). Has no other possibilities.

***Mafia*** is an organized group of players opposing civilians. During the day, he can participate
in voting and discussion, imitating civilians or other roles. At night, he can choose one player
to “kill”.

***Doctor*** – a civilian. Action at night – saving the specified player from “murder” (prevents `mafia`
action).
*In order for the role to work, it must be one of the first called in the app (along with the `maniac` and `thief`)*.

***Detective*** – a civilian. Action at night – finding out the role of the specified player (the moderator
tells him with signs whether the specified player belongs to the mafia or not). Can use the collected
information in the day's discussion. The role of the mafia boss cannot be revealed (the moderator will
give a “civilian” sign).

***Prostitute / beauty*** – a conditionally civilian. Action at night – saving the specified player.
(prevents `mafia` action). But, unlike the `doctor`, if a player
with this role dies, he “takes with him” the player he saved.

***Maniac*** – a conditionally civilian. Action at night – remove a living player from the vote. The victim
cannot vote for the next day. Also during the current night and the next day, victim cannot be killed
by anyone other than a `kamikaze`.
*In order for the role to work, it must be one of the first called in the app (along with the `thief` and `doctor`)*.

***Priest / immortal*** – a civilian. Has no effect. Can be killed only in three cases:
- when his role is stolen by a `thief`;
- when he ended up with a `beauty` whom the `mafia` came to;
- when a `kamikaze` points at him.

***Thief*** – a conditionally civilian. The action at night is to “disable the role” of the specified player.
The victim doesn’t know that her role has been “stolen” and acts as usual. But her actions will not have
an effect during the current night (the `doctor` and the `beauty` will not be able to save, the `detective`
will receive the wrong answer from the moderator, the `priest` will lose protection, the `maniac` will not
change the victim’s condition).
*In relation to the `mafia`, the use of the role is controversial, because it is represented by several players*.
*In order for the role to work, it must be one of the first called in the app (along with the `maniac` and `doctor`)*.

***Kamikaze*** – a conditionally civilian. The action during the day is to destroy one player along with
himself (both players leave the game). Usually has a spontaneous character, that is, it’s a source of randomness
in the game. Only victims of a `maniac` are immune.

***Mafia boss*** – part of the `mafia`. It doesn’t have a separate action, it plays the role of the `mafia`
in its composition. Protected from disclosure of the role by the `detective`. Counted as a `mafia` when
determining the victory of one of the teams.

&nbsp;

The following are allowed in the game:
- at least two, but not more than 50% of players with the `mafia` role (including the `mafia boss`);
- no more than one player in any role other than `townspeople`;
- unlimited number of `townspeople` (those left without a special role).

&nbsp;



### Features of the application

The ***initial application interface*** allows you to:
- select the interface language;
- configure app settings;
- access a full range of app and Lab help and support resources.

The ***interface for entering names and roles of players*** performs independent control of the distribution
of roles and the correctness of the input. In this case, the specified names are remembered for use in the next
game, and the specified roles are reset after exiting the current session.

The ***gaming activity tracking interface*** has the following capabilities:
- displaying tooltips when you hover over abbreviations in the “roles” and “statuses” columns;
- display of changing player statuses with color indication;
- displaying a dynamic context menu when pressing buttons in the “actions” column, which contains only those actions that are currently available;
- obvious transition between day and night phases with display of actions performed and their results;
- control of intersecting rules;
- control of timer with several preset intervals and audible alarm (60, 10 and 0 seconds);
- launch a random music track from the directory specified in the app settings (for night phases);
- collecting completed actions in a timeline;
- displaying messages about incorrect application of roles;
- displaying messages about team win / loss
