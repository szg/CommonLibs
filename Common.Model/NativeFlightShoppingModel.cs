using System;
using System.Collections.Generic;


public class NativeFlightShoppingModel
{
    /// <summary>
    /// 航班查询信息
    /// </summary>
    public NativeFlisthSearchInfo SearchInfo { get; set; }

    /// <summary>
    /// 航班列表
    /// </summary>
    public List<NativeFlight> Flights { get; set; }

    /// <summary>
    /// 航司列表
    /// </summary>
    public List<NativeAirLine> AirLines { get; set; }

    /// <summary>
    /// 机场列表
    /// </summary>
    public List<NativeAirPort> AirPorts { get; set; }

    /// <summary>
    /// 一周最低价
    /// </summary>
    public List<LowPriceModel> LowPriceWeek { get; set; }

    /// <summary>
    /// 行李退改签信息
    /// </summary>
    public List<BaggageAndRefundModel> BaggageAndRefunds { get; set; }

    /// <summary>
    /// 最低价
    /// </summary>
    public decimal LowestPrice { get; set; }

}
public class LowPriceModel
{
    public string dep { get; set; }
    public string arr { get; set; }
    public string date { get; set; }
    public decimal price { get; set; }
    public string title { get; set; }
    public string week { get; set; }

}
/// <summary>
/// 航班查询信息
/// </summary>
public class NativeFlisthSearchInfo
{

    /// <summary>
    /// 航班总数
    /// </summary>
    public int AvItemCount { get; set; }
    /// <summary>
    /// 出发城市
    /// </summary>
    public string DepartureCity { get; set; }

    /// <summary>
    /// 出发城市中文名称
    /// </summary>
    public string DepartureCityCnName { get; set; }

    /// <summary>
    /// 出发机场
    /// </summary>
    public string DepartureAirport { get; set; }

    /// <summary>
    /// 到达城市
    /// </summary>
    public string ArrivalCity { get; set; }

    /// <summary>
    /// 到达城市中文名称
    /// </summary>
    public string ArrivalCityCnName { get; set; }

    /// <summary>
    /// 到达机场
    /// </summary>
    public string ArrivalAirport { get; set; }
    /// <summary>
    /// 出发日期
    /// </summary>
    public string DepartureDate { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public string CreateDate { get; set; }

    /// <summary>
    /// 本次查询时间
    /// </summary>
    public double ExecuteTime { get; set; }

    /// <summary>
    /// 改签过滤价
    /// </summary>
    public decimal ChangePrice { get; set; }

}

/// <summary>
/// 航班
/// </summary>
public class NativeFlight
{
    /// <summary>
    /// 航班号
    /// </summary>
    public string Flight { get; set; }

    /// <summary>
    /// 航司
    /// </summary>
    public string Airline { get; set; }
    /// <summary>
    /// 到达航站楼
    /// </summary>
    public string ArrivalTerminal { get; set; }

    /// <summary>
    /// 航司对象
    /// </summary>
    public NativeAirLine AirLineObj { get; set; }

    /// <summary>
    /// 到达时间
    /// </summary>
    public DateTime ArrivalDate { get; set; }
    /// <summary>
    /// 到达机场
    /// </summary>
    public string ArrivalAirport { get; set; }

    /// <summary>
    /// 到达机场对象
    /// </summary>
    public NativeAirPort ArrivalAirPortObj { get; set; }

    /// <summary>
    /// 是否跨天
    /// </summary>
    public bool IsCrossDay { set; get; }
    /// <summary>
    /// 出发航站楼
    /// </summary>
    public string DepartureTerminal { get; set; }
    /// <summary>
    /// 出发时间
    /// </summary>
    public DateTime DepartureDate { get; set; }
    /// <summary>
    /// 出发时间段  凌晨 上午 下午 晚上
    /// </summary>
    public string DepartureTimePart { get; set; }
    /// <summary>
    /// 出发机场
    /// </summary>
    public string DepartureAirport { get; set; }

    /// <summary>
    /// 出发机场对象
    /// </summary>
    public NativeAirPort DepartureAirportObj { get; set; }

    /// <summary>
    /// 舱位列表
    /// </summary>
    public List<NativeCabin> CabinList { get; set; }

    /// <summary>
    /// 最低价舱位
    /// </summary>
    public NativeCabin LowCabin { get; set; }

    /// <summary>
    /// 经停信息
    /// </summary>
    public List<StopInfo> StopInfo { get; set; }
    /// <summary>
    /// 机型
    /// </summary>
    public string Plane { get; set; }
    /// <summary>
    /// 机型描述
    /// </summary>
    public string AircraftType { get; set; }
    /// <summary>
    /// 餐食标识
    /// </summary>
    public string Meal { get; set; }

    /// <summary>
    /// 餐食标识 尽量用这个  true有餐食，false无餐食
    /// </summary>
    public bool HasMeal { get; set; }

    /// <summary>
    /// 是否共享航班
    /// </summary>
    public bool IsCodeShare { get; set; }

    /// <summary>
    /// 实际承运航司二字码
    /// </summary>
    public string ShareAirLineCode { get; set; }

    /// <summary>
    /// 实际承运航司对象
    /// </summary>
    public NativeAirLine ShareAirLineObj { get; set; }

    /// <summary>
    /// 共享航班号
    /// </summary>
    public string ShareFlightNo { get; set; }

    /// <summary>
    /// 里程
    /// </summary>
    public int TPM { get; set; }

    /// <summary>
    /// 查询更多价格加密串
    /// </summary>
    public string FlightEx { get; set; }

    /// <summary>
    /// 准点率
    /// </summary>
    public string OnTimeRate { get; set; }
}

/// <summary>
/// 航司
/// </summary>
public class NativeAirLine
{
    /// <summary>
    /// 航司
    /// </summary>
    public string Ariline { get; set; }
    /// <summary>
    /// 航司中文
    /// </summary>
    public string ArilineName_CN { get; set; }
    /// <summary>
    /// 航司英文
    /// </summary>
    public string ArilineName_EN { get; set; }
    /// <summary>
    /// 缩写
    /// </summary>
    public string AbbreviationName { get; set; }
}

/// <summary>
/// 机场
/// </summary>
public class NativeAirPort
{
    /// <summary>
    /// 机场三字码
    /// </summary>
    public string AirPortCode { get; set; }
    /// <summary>
    /// 机场中文  
    /// </summary>
    public string AirPortName_CN { get; set; }
    /// <summary>
    /// 机场英文
    /// </summary>
    public string AirPortName_EN { get; set; }
    /// <summary>
    /// 机场缩写
    /// </summary>
    public string AirPortAbbreviationName { get; set; }
    /// <summary>
    /// 城市三字码
    /// </summary>
    public string AirPortCityCode { get; set; }
    /// <summary>
    /// 城市中文
    /// </summary>
    public string AirPortCityName_CN { get; set; }
    /// <summary>
    /// 城市英文
    /// </summary>
    public string AirPortCityName_EN { get; set; }
}


/// <summary>
/// 舱位信息
/// </summary>
public class NativeCabin
{

    /// <summary>
    /// 舱位唯一ID
    /// </summary>
    public string CabinID { get; set; }

    /// <summary>
    /// 舱位代码
    /// </summary>
    public string Class { get; set; }

    /// <summary>
    /// 舱位类型
    /// </summary>
    public string ClassType { get; set; }
    /// <summary>
    /// 舱位名称
    /// </summary>
    public string ClassName { get; set; }

    /// <summary>
    /// 子舱位名称
    /// </summary>
    public string SubClassName { get; set; }

    /// <summary>
    /// 剩余数量
    /// </summary>
    public string SeatNumber { get; set; }


    /// <summary>
    /// 成人舱位价格
    /// </summary>
    public CabinPrice AdultCabinPrice { get; set; }

    /// <summary>
    /// 儿童舱位价格
    /// </summary>
    public CabinPrice ChdCabinPrice { get; set; }

    /// <summary>
    /// 婴儿舱位价格
    /// </summary>
    public CabinPrice InfCabinPrice { get; set; }

    /// <summary>
    /// 政策信息
    /// </summary>
    public PolicyInfo Policy { get; set; }

    /// <summary>
    /// 价格类型
    /// </summary>
    public CabinPriceTypeOptions CabinPriceType { get; set; }

    /// <summary>
    /// 价格标签（仅使用于操作端展示）
    /// </summary>
    public string PriceDisplayTag { get; set; }

    /// <summary>
    /// 价格来源
    /// </summary>
    public CabinPriceSourceOptions CabinPriceSource { get; set; }

    /// <summary>
    /// 扩展字段1
    /// </summary>
    public string Ex1 { get; set; }

    /// <summary>
    /// 扩展字段2
    /// </summary>
    public string Ex2 { get; set; }

    /// <summary>
    /// 客户端价格类型描述
    /// </summary>
    public string ClientCabinPriceTypeDesc { get; set; }

    /// <summary>
    /// 操作端价格类型描述
    /// </summary>
    public string OperationCabinPriceTypeDesc { get; set; }

    /// <summary>
    /// 限制的证件类型
    /// </summary>
    public List<CredentialTypeOptions> LimitCredentialTypes { get; set; }

}

public enum CabinPriceTypeOptions
{
    公布运价 = 0,
    私有运价 = 1,
    三方价格 = 2,
    两方价格 = 3,
    大众价格 = 4,
    直加价格 = 5,
    官网旗舰 = 6,
    官网代购 = 7,
    蜗牛直营 = 8
}
public enum CabinPriceSourceOptions
{
    IBE = 0,
    FD = 1,
    蜗牛 = 2
}
/// <summary>
/// 证件类型
/// </summary>
public enum CredentialTypeOptions
{
    身份证 = 1,
    护照 = 2,
    学生证 = 3,
    军人证 = 4,
    回乡证 = 5,
    外国人永久居留身份证 = 6,
    港澳通行证 = 7,
    台湾通行证 = 8,
    国际海员证 = 9,
    台胞证 = 10,
    其他 = 11,
    港澳台居民居住证 = 12
}

/// <summary>
/// 政策信息
/// </summary>
public class PolicyInfo
{

    /// <summary>
    /// 佣金Id
    /// </summary>
    public Guid CommissionId { get; set; }

    /// <summary>
    /// 佣金类型
    /// </summary>
    public CommissionTypeEnum CommissionType { get; set; }

    /// <summary>
    /// 协议信息
    /// </summary>
    public ProtocolInfoModel Protocol { get; set; }
}

/// <summary>
/// 协议信息
/// </summary>
public class ProtocolInfoModel
{

    /// <summary>
    /// 协议Id
    /// </summary>
    public Guid ProtocolId { get; set; }

    /// <summary>
    /// 协议号
    /// </summary>
    public string ProtocolNum { get; set; }

    /// <summary>
    /// 协议名称
    /// </summary>
    public string ProtocolName { get; set; }

    /// <summary>
    /// 协议绑定类型：1自签、2明固定、3明浮动、4暗浮动
    /// </summary>
    public CommissionProtocolTypeEnum CommissionProtocolType { get; set; }

    /// <summary>
    /// 航司
    /// </summary>
    public string Airlines { get; set; }

    /// <summary>
    /// 协议类型  0 两方， 1 三方， 2 大众
    /// </summary>
    public ProtocolTypeEnum ProtocolType { get; set; }

    /// <summary>
    /// 折扣信息id
    /// </summary>
    public Guid DiscountId { get; set; }

    /// <summary>
    /// 折扣  值例如：80
    /// </summary>
    public decimal Discount { get; set; }

}

/// <summary>
/// 佣金类型
/// </summary>
public enum CommissionTypeEnum
{

    /// <summary>
    /// 正常
    /// </summary>
    Normal = 1,

    /// <summary>
    /// 两方
    /// </summary>
    Bilateral = 2,

    /// <summary>
    /// 三方
    /// </summary>
    Tripartite = 3,

    /// <summary>
    /// 特殊
    /// </summary>
    Special = 4,

    /// <summary>
    /// 官网
    /// </summary>
    Offical = 5

}

/// <summary>
/// 协议绑定类型
/// </summary>
public enum CommissionProtocolTypeEnum
{

    /// <summary>
    /// 自签
    /// </summary>
    ZiQian = 1,

    /// <summary>
    /// 明固定
    /// </summary>
    MingGuDing = 2,

    /// <summary>
    /// 明浮动
    /// </summary>
    MingFuDong = 3,

    /// <summary>
    /// 暗浮动
    /// </summary>
    AnFuDong = 4
}

public enum ProtocolTypeEnum
{
    两方 = 0,
    三方 = 1,
    大众 = 2
}

/// <summary>
/// 舱位价格
/// </summary>
public class CabinPrice
{

    /// <summary>
    /// 票面价格
    /// </summary>
    public decimal PiaoMianPrice { get; set; }

    /// <summary>
    /// Y舱全价
    /// </summary>
    public decimal YFullPrice { get; set; }

    /// <summary>
    /// 全价
    /// </summary>
    public decimal FullPrice { get; set; }

    /// <summary>
    /// 原价
    /// </summary>
    public decimal OriginalPrice { get; set; }

    /// <summary>
    /// 运价基础代码
    /// </summary>
    public string FareBasis { get; set; }

    /// <summary>
    /// 机场税
    /// </summary>
    public decimal AirportTax { get; set; }

    /// <summary>
    /// 燃油税
    /// </summary>
    public decimal FuelSurcharge { get; set; }

    /// <summary>
    /// 定额（代理费）
    /// </summary>
    public string AgencyFees { get; set; }

    /// <summary>
    /// 后返
    /// </summary>
    public string BackFees { get; set; }

    /// <summary>
    /// 内部产品特色 直加价格有值
    /// </summary>
    public string FeatureOfProduct { get; set; }

    /// <summary>
    /// 内部产品描述 直加价格有值
    /// </summary>
    public string InternalProductDescription { get; set; }

    /// <summary>
    /// 外部产品描述 直加价格有值
    /// </summary>
    public string ExternalProductDescription { get; set; }

    /// <summary>
    /// 行李退改
    /// </summary>
    public string BaggageAndRefund_Key { get; set; }

    /// <summary>
    /// 展示价
    /// </summary>
    public decimal ZhanShiPrice { get; set; }

    /// <summary>
    /// 省多少钱比较基线
    /// </summary>
    public decimal EcoBasicPrice { get; set; }

    /// <summary>
    /// 省多少钱
    /// </summary>
    public decimal EcoPrice
    {
        get
        {
            return EcoBasicPrice - ZhanShiPrice;
        }
    }

    /// <summary>
    /// 折扣
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// 单位前返金额
    /// </summary>
    public decimal CmpBeforeRebate { get; set; }

    /// <summary>
    /// 单位后返金额
    /// </summary>
    public decimal CmpBackRebate { get; set; }

    /// <summary>
    /// 个人后返金额
    /// </summary>
    public decimal IndividualBackRebate { get; set; }

    /// <summary>
    /// 线上服务费金额
    /// </summary>
    public decimal OnlineServiceCharge { get; set; }

    /// <summary>
    /// 线下服务费金额
    /// </summary>
    public decimal OfflineServiceCharge { get; set; }

    /// <summary>
    /// 渠道费
    /// </summary>
    public decimal ChannelCharge { get; set; }

    /// <summary>
    /// 因私服务费
    /// </summary>
    public decimal ForPrivateCharge { get; set; }

    /// <summary>
    /// 线上改期服务费
    /// </summary>
    public decimal OnlineRenewalFeeServiceCharge { get; set; }

    /// <summary>
    /// 线下改期服务费
    /// </summary>
    public decimal OfflineRenewalFeeServiceCharge { get; set; }

    /// <summary>
    /// 外部产品特色 直加价格有值
    /// </summary>
    public string ExternalFeatureOfProduct { get; set; }

}

/// <summary>
/// 经停信息
/// </summary>
public class StopInfo
{
    /// <summary>
    /// 离开时间
    /// </summary>
    public DateTime DepartTime { get; set; }

    /// <summary>
    /// 到达时间
    /// </summary>
    public DateTime ArrivalTime { get; set; }

    /// <summary>
    /// 经停城市Code
    /// </summary>
    public string CityCode { get; set; }

    /// <summary>
    /// 经停城市名称
    /// </summary>
    public string CityName { get; set; }
    /// <summary>
    /// 经停城市英文名
    /// </summary>
    public string CityNameEn { get; set; }

    /// <summary>
    /// 经停机场三字码
    /// </summary>
    public string AirportCode { get; set; }

    /// <summary>
    /// 经停机场名
    /// </summary>
    public string AirportName { get; set; }

}

/// <summary>
/// 行李退改描述
/// </summary>
public class BaggageAndRefundModel
{
    /// <summary>
    /// key
    /// </summary>
    public string Key { get; set; }
    /// <summary>
    /// 行李
    /// </summary>
    public string Baggage { get; set; }
    /// <summary>
    /// 行李描述
    /// </summary>
    public string BaggageRegulation { get; set; }
    /// <summary>
    /// 更改规定
    /// </summary>
    public string ChangePolicy { get; set; }
    /// <summary>
    /// 更改规定英文
    /// </summary>
    public string ChangePolicyEN { get; set; }
    /// <summary>
    /// 退票规定
    /// </summary>
    public string RefundPolicy { get; set; }
    /// <summary>
    /// 退票规定英文
    /// </summary>
    public string RefundPolicyEN { get; set; }
    /// <summary>
    /// 签转
    /// </summary>
    public string ReissuePolicy { get; set; }
    /// <summary>
    /// 签转英文
    /// </summary>
    public string ReissuePolicyEN { get; set; }
    /// <summary>
    /// 其他规定
    /// </summary>
    public string OtherPolicy { get; set; }

}
