import json
import socket
import bluetooth

PORT = 25595

socket = socket.socket()
socket.bind(("localhost", PORT))

if __name__ == "__main__":
    while True:
        print("Bluetooth Discovery server started, awaiting connection")
        socket.listen(5)
        conn, addr = socket.accept()
        print(f"Received connection from: {conn} - {addr}")

        nearby_devices = bluetooth.discover_devices(duration=4, lookup_names=True)
        print(f"Found {len(nearby_devices)} devices.")

        devices = [{"Address": addr, "Name": name} for addr, name in nearby_devices]
        msg = json.dumps(devices)
        msg = bytes(msg, encoding="utf-8")

        print(f"Sent bluetooth info to {addr}")
        conn.sendall(msg)