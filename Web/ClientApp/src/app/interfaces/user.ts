export interface UserDto {
    name: string;
    password: string | null;
    avatarUrl: string;
    bio: string;
}
  
export interface User {
    id: string;
    name: string;
    password: string;
    avatarUrl: string;
    bio: string;
}
  
export interface LoginCredential {
    name: string;
    password: string;
}
  
export interface LoginResponse {
    isLoginSuccessful: boolean;
    errorMessage: string;
    token: string;
}
  
export interface SignupResponse {
    isSignupSuccessful: boolean;
    errorMessage: string;
    token: string;
}