#pragma once



NTSTATUS
DsHidMini_GetInputReport(
    _In_ DMFMODULE DmfModule,
    _In_ WDFREQUEST Request,
    _In_ HID_XFER_PACKET* Packet,
    _Out_ ULONG* ReportSize
);

NTSTATUS
DsHidMini_RetrieveNextInputReport(
    _In_ DMFMODULE DmfModule,
    _In_ WDFREQUEST Request,
    _Out_ UCHAR** Buffer,
    _Out_ ULONG* BufferSize
);

NTSTATUS
DsHidMini_GetFeature(
    _In_ DMFMODULE DmfModule,
    _In_ WDFREQUEST Request,
    _In_ HID_XFER_PACKET* Packet,
    _Out_ ULONG* ReportSize
);

NTSTATUS
DsHidMini_SetFeature(
    _In_ DMFMODULE DmfModule,
    _In_ WDFREQUEST Request,
    _In_ HID_XFER_PACKET* Packet,
    _Out_ ULONG* ReportSize
);

NTSTATUS
DsHidMini_SetOutputReport(
    _In_ DMFMODULE DmfModule,
    _In_ WDFREQUEST Request,
    _In_ HID_XFER_PACKET* Packet,
    _Out_ ULONG* ReportSize
);

NTSTATUS
DsHidMini_WriteReport(
    _In_ DMFMODULE DmfModule,
    _In_ WDFREQUEST Request,
    _In_ HID_XFER_PACKET* Packet,
    _Out_ ULONG* ReportSize
);