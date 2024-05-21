export interface IHttpClient {
  checkAuth(): boolean;
  setAuth(token: any): void;
  resetAuth(): void;
  post(url: string, body: any): any;
  get(url: string): any;
}
