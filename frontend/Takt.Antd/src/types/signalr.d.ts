export interface ITaktSignalRMessage {
    type: string;
    title: string;
    content: string;
    timestamp: Date;
}

export interface ITaktSignalRClient {
    receiveMailStatus: (notification: ITaktSignalRMessage) => void;
    receiveNoticeStatus: (notification: ITaktSignalRMessage) => void;
    receiveTaskStatus: (notification: ITaktSignalRMessage) => void;
    receivePersonalNotice: (notification: ITaktSignalRMessage) => void;
    receiveBroadcast: (notification: ITaktSignalRMessage) => void;
    receiveHeartbeat: (timestamp: Date) => void;
    receiveKickout: (message: string) => void;

    // 工作流通知
    receiveWorkflowStarted: (notification: ITaktSignalRMessage) => void;
    receiveWorkflowApproval: (notification: ITaktSignalRMessage) => void;
    receiveWorkflowStatusChanged: (notification: ITaktSignalRMessage) => void;
    receiveWorkflowCompleted: (notification: ITaktSignalRMessage) => void;
    receiveWorkflowException: (notification: ITaktSignalRMessage) => void;
    receiveWorkflowNodeReached: (notification: ITaktSignalRMessage) => void;
    receiveWorkflowTimeout: (notification: ITaktSignalRMessage) => void;
    receiveWorkflowDelegate: (notification: ITaktSignalRMessage) => void;
    receiveWorkflowTransfer: (notification: ITaktSignalRMessage) => void;
} 
