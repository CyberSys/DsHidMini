#include "Driver.h"
#include "DsBth.tmh"
#include <bluetoothapis.h>
#include <bthioctl.h>

NTSTATUS DsBth_SendDisconnectRequest(PDEVICE_CONTEXT Context)
{
	BLUETOOTH_ADDRESS address;
	WDF_MEMORY_DESCRIPTOR memDesc;
	UCHAR buffer[sizeof(BLUETOOTH_ADDRESS)];

	RtlZeroMemory(buffer, sizeof(BLUETOOTH_ADDRESS));
	RtlCopyMemory(buffer, &Context->DeviceAddress, sizeof(Context->DeviceAddress));
	
	address.ullLong = *(PULONGLONG)&buffer[0];

	WDF_MEMORY_DESCRIPTOR_INIT_BUFFER(
		&memDesc,
		&address,
		sizeof(BLUETOOTH_ADDRESS)
	);

	//
	// Send disconnect request
	// 
	return WdfIoTargetSendIoctlSynchronously(
		Context->Connection.Bth.BthIoTarget,
		NULL, // use internal request object
		IOCTL_BTH_DISCONNECT_DEVICE,
		&memDesc, // holds address to disconnect
		NULL,
		NULL,
		NULL
	);
}