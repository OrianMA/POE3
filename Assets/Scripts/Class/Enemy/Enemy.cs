using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellLink;

public class Enemy : AMovableEntity, Ihealth
{
    private void Start()
    {
        //usableSpell[0] = usableSpell[0].Clone();
        usableSpell.Add(new KeyValuePair<SpellLink.SpellKey, ASpell>(SpellLink.SpellKey.IAEnemy, SpellManager.Instance.GetSpell(SpellLink.SpellKey.IAEnemy)));


        //usableSpell[0].Use(this,GameManager.instance.playerController._Player, new object[] { _agent });

        foreach (KeyValuePair<SpellLink.SpellKey, ASpell> spell in usableSpell)
        {
            if (spell.Key == SpellLink.SpellKey.IAEnemy)
            {
                spell.Value.Use(this, GameManager.instance.playerController._Player, new object[] { _agent });
            }
        }


    }

    private void Update()
    {
        foreach (KeyValuePair<SpellLink.SpellKey, ASpell> spell in usableSpell)
        {
            if (spell.Key == SpellLink.SpellKey.IAEnemy)
            {
                spell.Value.Tick(Time.deltaTime);
            }
        }
    }

}
