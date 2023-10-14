using UnityEngine;

public class Bullet : MonoBehaviour
{
    BaseController _launcher;
    BaseController _target;
    float _moveSpeed = 15f;

    public void SetInfo(BaseController launcher, BaseController target)
    {
        _launcher = launcher;
        _target = target;
    }

    private void Update()
    {
        Vector3 dir = _target.transform.position - transform.position;
        transform.position += dir.normalized * _moveSpeed * Time.deltaTime;
        transform.LookAt(_target.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _target.gameObject)
        {
            switch (_launcher.WorldObjectType)
            {
                case Define.WorldObject.Unit:
                    {
                        Define.Unit type = (_launcher as UnitController).Type;
                        GameObject go = Managers.Resource.Instantiate($"Vfx/Bullet/{type}Bullet_Hit", transform.position, Quaternion.identity);
                        Managers.Resource.Destory(go, 0.5f);
                        _target.OnDamaged(_launcher);
                        Managers.Resource.Destory(gameObject);
                    }
                    break;
                case Define.WorldObject.Tower:
                    {
                        Define.Tower type = (_launcher as TowerController).Type;
                        GameObject go = Managers.Resource.Instantiate("Vfx/Bullet/TowerBullet_Hit", transform.position, Quaternion.identity);
                        Managers.Resource.Destory(go, 0.5f);

                        if (type == Define.Tower.Focus)
                        {
                            go.transform.localScale = new Vector3(3 * Define.TILE_SIZE, 3 * Define.TILE_SIZE, 3 * Define.TILE_SIZE);

                        }
                        else
                        {
                            _target.OnDamaged(_launcher);
                            Managers.Resource.Destory(gameObject);
                        }
                    }
                    break;
            }
        }
    }
}

