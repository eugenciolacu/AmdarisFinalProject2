export interface UserResponse {
    isSuccess: boolean;
    token: string;
    tokenExpiration: Date;
    maxRole: string;
    firstName: string;
    lastName: string;
}