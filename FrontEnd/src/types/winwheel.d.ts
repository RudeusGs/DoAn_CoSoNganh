// src/types/winwheel.d.ts
declare module 'winwheel' {
    export default class Winwheel {
        ctx: any;
        centerX: any;
        centerY: any;
        outerRadius: any;
        constructor(options: any);
        startAnimation(): void;
        stopAnimation(forceStop?: boolean): void;
        resetAnimation(): void;
        draw(): void;
    }
}
