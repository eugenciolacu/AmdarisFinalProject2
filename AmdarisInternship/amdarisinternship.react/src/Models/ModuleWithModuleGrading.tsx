import { Module } from "./Module";
import { ModuleGrading } from "./ModuleGrading";

export interface ModuleWithModuleGrading {
    module: Module,
    moduleGradings: ModuleGrading[],
}