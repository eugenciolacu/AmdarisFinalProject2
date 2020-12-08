export interface AddModuleForm {
    nameM: string;
    moduleGradings: ModuleGradingForm[];
}

export interface ModuleGradingForm {
    name: string;
    weight: number;
}