= Combat Damage =

The purpose of this page is to provide more details about how damage is actually calculated when it comes to combat.  The data on this page is not 100% accurate, but provides a good foundation for a more in-depth understanding of how combat damage is calculated.

-*What is not taken into account in the calculations on this page?**

Some combat bonuses that come from Gear, Avatar Score bonuses, relics, etc.  These are typically buffs that apply only in specific scenarios, such as in Explorations, only in the First round, etc.  

-Examples of buffs missing from calculations on this page:*

1. Combat Damage for battles in the Fissure will not take into account the bonuses given by the **Home Facilities Avatar Buff**.
1. If you have Golden Drazgul Fang, calculations below do not account for the bonus <code>CRIT DMG</code> in Round 1 given by this gear.

1. Combat Stats
The main combat stats are your HARD stats (HP, ATK, RUSH, and DEF) as well as your ELMT Stats (ELMT DMG and ELMT RES).

: > <code>RES</code> = Resistance
: > <code>DMG</code> = Damage
:: **Note:** For elements (ex: <code>Fire DMG</code>) it is a stat that you accumulate; however when referred to as <code>DMG</code>, <code>ATK DMG</code>, <code>RUSH DMG</code>, <code>CRIT DMG</code> or <code>Total DMG</code> it refers to a value calculated from your stats.

[WIKITABLE OMITTED]

1. ATK Damage (First Attack)
ATK Damage is done every round, regardless of if you spot an enemy's weakness.  The actual <code>DMG</code> done in the attack is based a combination of <code>ATK DMG</code> and <code>ELMT DMG</code>.  Your <code>ATK</code> stat will scale the amount of <code>ELMT DMG</code> you do during your first attack.

The high level formula for your first round of attack is:

<pre>
TOTAL DMG = ATK DMG + CRIT DMG + ELMT DMG
</pre>

''For a detailed breakdown of how to calculate <code>TOTAL DMG</code>, see Total ATK or RUSH DMG Calculation

1. RUSH Damage (Second Attack)
<blockquote>
-*Fun Fact:**<br>
The translation of "rush" from the original Chinese game text would be better translated as "pursuit" in english (according to google translate).  This is why a "rush" attack is actually the second attack and not an attack that happens before the main attack as you might assume from the name.  

Presumably it was translated as "rush" instead of "pursuit" to make the stats acronym "HARD" more memorable -- but this is pure conjecture.
</blockquote>

-*Note:** The **majority of the <code>TOTAL DMG</code> in the second attack typically comes from <code>ELMT DMG</code>.**  This is because your <code>RUSH</code> stat is generally significantly less than your <code>ATK</code> stat, but the the enemy's <code>DEF</code> stat applies to <code>RUSH</code> as well.

RUSH Damage is only done if you spot an enemy's weakness.  The actual <code>DMG</code> done in the attack is based a combination of <code>RUSH DMG</code> and <code>ELMT DMG</code>.  Your <code>RUSH</code> stat will scale the amount of <code>ELMT DMG</code> you do during your second attack.

The high level formula for your second attack in a single round is:

<pre>
TOTAL DMG = RUSH DMG + CRIT DMG + ELMT DMG
</pre>

-For a detailed breakdown of how to calculate <code>TOTAL DMG</code>, see Total ATK or RUSH DMG Calculation*

1. Elemental Damage
<blockquote>
-*Credit for all Elemental Damage calculations goes to the article Strategy: Use mathematics to teach you how to deal with elemental damage  by Li Ruosheng **
</blockquote>

For most player in mid to late game, **1 point of <code>ELMT DMG</code> can be assumed to be equivalent to about 7 <code>ATK</code>**.  Because <code>ELMT DMG</code> is also calculated for <code>RUSH</code> attacks, each point is **more useful than <code>ATK</code>**.  For Non-PvP battles, opponents also **DO NOT** have any <code>ELMT RES</code>.

Elemental Damage scales with either the attacker's <code>ATK</code> stat or <code>RUSH</code> stat depending on if the calculation is for a regular attack or a rush attack.  As your <code>ATK</code> and/or <code>RUSH</code> stats grow, the amount of additional damage done for each <code>ELMT DMG</code> point increases at a slower and slower rate (logarithmic scale).  At approximately **80,000** <code>ATK</code> or <code>RUSH</code> each point of <code>ELMT DMG</code> equates to about **7 DMG** (not accounting for resistances).  Each <code>ELMT DMG</code> point equates to about **8 DMG** once your <code>ATK</code> or <code>RUSH</code> reach between **390,000** and **400,000**.  (See the table below for a breakdown of how <code>ELMT DMG</code> scales)

-For a detailed breakdown of how to calculate <code>TOTAL DMG</code>, see Elemental Damage Calculation*<br>
-For a quick-reference table to see how <code>ELMT DMG</code> scales at a glance, see Elemental DMG Scaling Reference Table*

= Appendix =

1. Glossary
Sometimes it can get confusing when you read a statement about a stat such as <code>ELMT DMG</code>.  Is this the final damage after scaling?  Is this the sum of the raw elemental DMG stats?

To clear this confusion (at least for this page), here is the glossary for the values used on this page:

[WIKITABLE OMITTED]

1. Formulas / Calculations
1. Total ATK or RUSH DMG Calculation
The same general formula is used for <code>RUSH</code>, just switch out values such as <code>attacker.ATK</code> for <code>attacker.RUSH</code>, and the formula is the same.

<pre>
// Note: Round number in this formula is 1 based, meaning it goes from 1-20 (not 0-19 as programmers may assume)
function calculateAttackDamage(attacker: Combatant, defender: Combatant, roundNumber: number, crit: boolean) {

  const attackerAdjustedATK = (
    attacker.ATK 
    + attacker.combatbuff.beforeCombat.ATK 
    + (roundNumber * attacker.buffEachRound.ATK)
  );

  const defenderAdjustedDEF = (defender.DEF * (1 - attacker.effectsTowardsEnemy.ignoreDEF))

  // Note: The minimum value for rawAtkDmg is 5% of adjusted ATK 

  const rawAtkDmg = Math.max(
    attackerAdjustedATK - defenderAdjustedDEF,
    attackerAdjustedATK * 0.05
  );

  // Note: Negative Element Resistances are not allowed.

  const elementalDmg = calculateElementalDamage(attacker, defender, rawAtkDmg)
  
  let dmgMultiplier = 1 + attacker.effectsTowardsEnemy.DMG

  if (roundNumber === 1) {
    dmgMultiplier = dmgMultiplier + attacker.buffEachRound.roundOneDMG
  }

  const critMultiplier = (0.5 + attacker.enhanceSelf.critDMG) * (1 - defender.enhanceSelf.critDmgReduc)

  let critDmg = 0
  
  if (crit) {
    critDmg = rawAtkDmg * critMultiplier
  }
  
  return (rawAtkDmg * dmgMultiplier + elementalDamage) * (1 - defender.enhanceSelf.dmgReduc) + critDmg
}
</pre>

1. Elemental Damage Calculation
This is pseudo-code for elemental damage calculation:

<pre>
function calculateElementalDamage(attacker: Combatant, defender: Combatant, atkOrRush: number) {

  // Note: Negative Element Resistances are not allowed, so default to 0
  // if the attacker has higher ignore RES than the defender's RES.
  
  const fireDmg = attacker.elmtDMG.fire - Math.max(
    defender.elmtRES.fire - attacker.effectsTowardsEnemy.ignoreElmtRES.fire,
    0
  );

  const earthDmg = attacker.elmtDMG.earth - Math.max(
    defender.elmtRES.earth - attacker.effectsTowardsEnemy.ignoreElmtRES.earth,
    0
  );

  const waterDmg = attacker.elmtDMG.water - Math.max(
    defender.elmtRES.water - attacker.effectsTowardsEnemy.ignoreElmtRES.water,
    0
  );

  const windDmg = attacker.elmtDMG.earth - Math.max(
    defender.elmtRES.wind - attacker.effectsTowardsEnemy.ignoreElmtRES.wind,
    0
  );

  const poisonDmg = attacker.elmtDMG.earth - Math.max(
    defender.elmtRES.poison - attacker.effectsTowardsEnemy.ignoreElmtRES.poison,
    0
  );

  const elemDmg = fireDmg + waterDmg + earthDmg + windDmg + poisonDmg;

  return elemDmg * Math.log(atkOrRush) / Math.log(5);
}
</pre>

1. Elemental DMG Scaling Reference Table
-*How do I read this table?**

Example:<br>
Assume you are trying to estimate the total elemental damage you will do if you have <code>101,000</code> <code>ATK</code>, and <code>2,500</code> total <code>ELMT DMG</code> (<code>Fire DMG + Water DMG + Earth DMG + Wind DMG + Poison DMG</code>) for your first attack (not the <code>RUSH</code> attack).

1. Lookup the ATK value in the table closest to your <code>ATK</code>
1. In this case, you would go to the row for <code>100,000</code> ATK or RUSH
1. Multiply your total <code>ELMT DMG</code> (2,500) by the value in the second column (7.153) to get your estimate for <code>Total ELMT DMG</code> in your first attack:
1. <code>Total ELMT DMG = 2500 * 7.152 = 17,880</code>

[WIKITABLE OMITTED]

1. Content References
<references />

Category:Guides