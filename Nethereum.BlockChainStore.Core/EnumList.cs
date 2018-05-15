using System;
using System.Collections.Generic;
using System.Text;

namespace OpsICO.Core.Enums
{
    public enum CampaignType
    {
        ICO = 1,
        PreSale,
        PreICO
    }

    public enum ApprovalStatus
    {
        WaitingForApproval = 1,
        Approved,
        Rejected
    }

    public enum TransactionStatus
    {
        Failure = 0,
        Success
    }

    public enum TransactionDirection
    {
        FromContract = 0,
        ToContract
    }

    public enum CampaignStatus
    {
        WaitingForStart = 1,
        Started,
        SoftCapReached,
        HardCapReached,
        Funded,
        Closed
    }

    public enum RecordState
    {
        Active = 1,
        Passive
    }

    public enum CommentStatus
    {
        WaitingForApproval = 1,
        Approved,
        Rejected
    }

    public enum ReferenceStatus
    {
        Registered = 1,
        Committed,
        Invested
    }

    public enum DocumentType
    {
        ID = 1,
        Passport,
        Other
    }

    public enum Currency
    {
        ETH = 1,
        JOINT,
        JNT
    }

    public enum LinkType
    {
        WhitePaper = 1,
        ExecutiveSummary,
        Facebook,
        Telegram,
        Twitter,
        Instagram,
        Email,
        Video,
        Other
    }

    public enum WhitelistStatus
    {
        Approved = 1,
        Invested,
        Rejected
    }
    public enum ContractType
    {
        ICO = 1,
        Token
    }

    public enum TokenType
    {
        PlatformToken = 1,
        IcoToken,
        AirdropToken,
        Other
    }
}
