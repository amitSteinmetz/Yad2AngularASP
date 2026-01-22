import { Injectable, signal } from '@angular/core';

export type NotificationType = 'success' | 'error' | 'info';

export interface Notification {
  id: number;
  type: NotificationType;
  message: string;
}

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private notifications = signal<Notification[]>([]);
  readonly currentNotifications = this.notifications.asReadonly();

  show(message: string, type: NotificationType = 'info', duration = 5000) {
    const id = Date.now();
    this.notifications.update((prev) => [...prev, { id, type, message }]);

    if (duration > 0) {
      setTimeout(() => {
        this.clear(id);
      }, duration);
    }
  }

  success(message: string, duration = 5000) {
    this.show(message, 'success', duration);
  }

  error(message: string, duration = 0) {
    this.show(message, 'error', duration);
  }

  clear(id: number) {
    this.notifications.update((prev) => prev.filter((n) => n.id !== id));
  }
}
