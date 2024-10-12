using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController contoller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    public GameObject testPrefab;

    private void Awake()
    {
        contoller = GetComponent<TopDownController>();
    }

    void Start()
    {
        contoller.OnAttackEvent += OnShoot;
        contoller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        aimDirection = newAimDirection;
    }

    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO RangedAttackSO = attackSO as RangedAttackSO;
        float projectilesAngleSpace = RangedAttackSO.multipleProjectilesAngel;
        int numberOfProjectilesPerShot = RangedAttackSO.numberOfProjectilesPerShot;

        // 중간부터 펼쳐지는게 아니라 minangle부터 커지면서 쏘는 것으로 설계했어요! 
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * RangedAttackSO.multipleProjectilesAngel;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            // 그냥 올라가면 재미없으니 랜덤으로 변하는 randomSpread를 넣었어요!
            float randomSpread = Random.Range(-RangedAttackSO.spread, RangedAttackSO.spread);
            angle += randomSpread;
            CreateProjectile(RangedAttackSO, angle);
        }
    }

    private void CreateProjectile(RangedAttackSO RangedAttackSO, float angle)
    {
        // 화살 생성 -> 다음강에서 구조개선을 위해 잠시 흉물스러운 이름 참아주세요!
        GameObject obj = Instantiate(testPrefab);

        // 발사체 기본 세팅
        obj.transform.position = projectileSpawnPosition.position;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), RangedAttackSO);

        // 다음강에서 개선 시 활용할 코드
        // obj.SetActive(true);
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        // 벡터 회전하기 : 쿼터니언 * 벡터 순
        return Quaternion.Euler(0, 0, degree) * v;
    }
}