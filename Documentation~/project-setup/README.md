# Snapshot konfigurasi project

Folder ini menyediakan konfigurasi pendamping karena root Git repo berada di folder `Assets`, sedangkan Unity memerlukan `Packages` dan `ProjectSettings` pada folder induk.

Snapshot diambil dari project **VR HIV Disclosure** pada **25 November 2026**:

- **Unity:** 6000.0.39f1.
- **Meta XR All-in-One SDK:** 81.0.0.
- **Render pipeline:** Universal Render Pipeline 17.0.3.
- **Target build:** Android, IL2CPP, ARM64.

`Packages/` berisi manifest dan lock paket. `ProjectSettings/` berisi versi editor serta pengaturan aplikasi, rendering, kualitas, input, audio, fisika, layer, scene build, Timeline, dan XR. Pengaturan akun cloud serta preferensi lokal editor tidak disertakan dalam snapshot ini. Unity dapat membuat pengaturan lain yang belum tersedia saat project pertama kali dibuka.

Salin kedua folder tersebut ke folder induk `Assets` sesuai [panduan setup pada README utama](../../README.md#menjalankan-project). Pada Unity Hub, pilih folder induk yang berisi `Assets`, `Packages`, dan `ProjectSettings`.

Gunakan profil **VR Standalone** dalam `Assets/Settings/Build Profiles/` untuk menyertakan `MenuScene` dan `MainGameScene` pada build. Snapshot daftar scene global hanya mengaktifkan `MenuScene`.

Jika versi engine, dependensi, atau pengaturan build berubah, perbarui snapshot ini dari `Packages/` dan `ProjectSettings/` project induk, lalu sesuaikan tabel versi pada README utama.
