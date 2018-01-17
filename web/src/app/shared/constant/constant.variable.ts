export class AppConstants {
  public static get AuthToken(): string { return 'auth_token'; }
  public static get RoleClaim(): string { return 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'; }
  public static get EmailClaim(): string { return 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'; }
  public static get NameClaim(): string { return 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'; }
  public static get IdClaim(): string { return 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid'; }
}
