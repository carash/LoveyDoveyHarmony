using UnityEngine;
using System.Collections;

public interface Socket {

	void Activate (ref int type, ref int heal, ref int damage, ref float lifeTime);
	void Spawn (int damage, float lifeTime, Transform parent);
}
