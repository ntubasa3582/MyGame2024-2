using System;
using UnityEngine;

public class CameraBillboard : MonoBehaviour
{
    /// <summary>
    /// 固定軸の定義
    /// </summary>
    private enum LockAxis
    {
        None,
        X,
        Y,
    }

    /// <summary>
    /// 固定する回転軸の指定
    /// </summary>
    [SerializeField] private LockAxis lockAxis;

    /// <summary>
    /// 正面方向を逆転するか否か
    /// (UIの場合はZ軸の負方向が正面になるので true を利用する)
    /// </summary>
    [SerializeField] private bool reverseFront;

    private void Update()
    {
        // 現オブジェクトからメインカメラ方向のベクトルを取得する
        Vector3 direction = Camera.main.transform.position - this.transform.position;

        // ベクトルの固定軸を考慮する
        Vector3 lockDirection = lockAxis switch
        {
            // ロック軸なしの場合はベクトルをそのまま利用する
            LockAxis.None => direction,
            // X軸固定の場合はX軸方向のベクトルの変量を0にする
            LockAxis.X => new Vector3(0.0f, direction.y, direction.z),
            // Y軸固定の場合はY軸方向のベクトルの変量を0にする
            LockAxis.Y => new Vector3(direction.x, 0.0f, direction.z),
            _ => throw new ArgumentOutOfRangeException()
        };

        // オブジェクトをベクトル方向に従って回転させる
        // (正面方向を逆転する場合はベクトルにマイナスをかける)
        transform.rotation = Quaternion.LookRotation(reverseFront ? -lockDirection : lockDirection);
    }
}