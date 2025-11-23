//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedProdOrder.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 生产工单数据提供类
//===================================================================

namespace Takt.Infrastructure.Data.Seeds.Biz.Logistics;

/// <summary>
/// 生产工单数据提供类
/// </summary>
public class TaktDbSeedProdOrder
{
    /// <summary>
    /// 获取默认生产工单数据
    /// </summary>
    /// <returns>生产工单数据列表</returns>
    public List<TaktProdOrder> GetDefaultProdOrders()
    {
        return new List<TaktProdOrder>
        {
            new TaktProdOrder { PlantCode = "C100", ProdOrderType = "ZDTA", ProdOrderCode = "453887", MaterialCode = "1921062000", ProdOrderQty = 1000, ProducedQty = 0, UnitOfMeasure = "PCS", Priority = 3, WorkCenter = "ZBSZ", ProdLine = "8", ProdBatch = "UR4MD258", Status = 0 },
            new TaktProdOrder { PlantCode = "C100", ProdOrderType = "ZDTA", ProdOrderCode = "453888", MaterialCode = "1921062001", ProdOrderQty = 500, ProducedQty = 0, UnitOfMeasure = "PCS", Priority = 2, WorkCenter = "ZBSZ", ProdLine = "8", ProdBatch = "UR4MD258", Status = 0 },
            new TaktProdOrder { PlantCode = "C100", ProdOrderType = "ZDTA", ProdOrderCode = "453889", MaterialCode = "1921062020", ProdOrderQty = 200, ProducedQty = 0, UnitOfMeasure = "PCS", Priority = 4, WorkCenter = "ZBSZ", ProdLine = "8", ProdBatch = "UR4MD258", Status = 0 },
            new TaktProdOrder { PlantCode = "C100", ProdOrderType = "ZDTA", ProdOrderCode = "453957", MaterialCode = "1921062022", ProdOrderQty = 100, ProducedQty = 0, UnitOfMeasure = "PCS", Priority = 1, WorkCenter = "ZBSZ", ProdLine = "8", ProdBatch = "UR4MD258", Status = 0 },
            new TaktProdOrder { PlantCode = "C100", ProdOrderType = "ZDTD", ProdOrderCode = "453848", MaterialCode = "1921062030", ProdOrderQty = 800, ProducedQty = 0, UnitOfMeasure = "PCS", Priority = 2, WorkCenter = "ZBSZ", ProdLine = "8", ProdBatch = "UR4MD258", Status = 0 }
        };
    }
}


