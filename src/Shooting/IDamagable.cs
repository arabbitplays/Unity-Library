using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    //return if bullet should be destroyed
    bool DoDamage(int damage);
}
